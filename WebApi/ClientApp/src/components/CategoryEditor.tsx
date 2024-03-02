import React, { ChangeEvent } from 'react';
import { Category } from '../objects/Category';
import { useLocation } from 'react-router-dom';
import { clientGet, clientPost, clientUpdate, clientUpload } from '../utils/client';
import { Alert, Avatar, Box, Button, FormControl, Grid, Input, InputLabel, MenuItem, Paper, Select, SelectChangeEvent, Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, Typography } from '@mui/material';
import { Item, tierColors, tierNames } from '../objects/Item';
import { Tier } from '../objects/enums';
import ClearIcon from '@mui/icons-material/Clear';
import SaveIcon from '@mui/icons-material/Save';

export interface ComponentProps {

}

const CategoryEditor: React.FC<ComponentProps> = ({

}) => {
	const categoryId = new URLSearchParams(useLocation().search).get('id');

	const getInitItem = (): Item => {
		return {
			id: '',
			title: '',
			description: '',
			tier: Tier.None,
			category: undefined,
			categoryId: categoryId,
			src: '',
		} as Item;
	};

	const [category, setCategory] = React.useState<Category>();
	const [items, setItems] = React.useState<Array<Item>>([]);
	const [item, setItem] = React.useState<Item>(getInitItem());
	const [error, setError] = React.useState<string | undefined>();

	React.useEffect(() => {
		clientGet(`category?id=${categoryId}`)
			.then((res) => {
				setCategory(res);
				setItems(res.items ? res.items : []);
			});
	}, []);

	const tiers = (Object.keys(Tier) as (keyof typeof Tier)[])
		.filter((q) => Number(q))
		.sort()
		.reverse() as Array<unknown> as Array<Tier>;

	const uploadImage = (event: ChangeEvent<HTMLInputElement>) => {
		event.preventDefault();

		if (!event.target.files) {
			return;
		}

		clientUpload('image', event.target.files[0]).then((res) => {
			setItem((prev) => {
				return {
					...prev,
					src: res.src
				};
			});
		}).catch((message) => {
			setError(message);
		});
	};

	const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		const data = new FormData(event.currentTarget);
		(item.id ? clientUpdate('item', {
			id: item.id,
			title: data.get('title'),
			description: data.get('description'),
			categoryId: item.categoryId,
			src: item.src,
			tier: +item.tier,
		}) : clientPost('item', {
			title: data.get('title'),
			description: data.get('description'),
			src: item.src,
			categoryId: item.categoryId,
			tier: +item.tier,
		})).then((res) => {
			setError(undefined);
			setItem(getInitItem());
			// loadCategories();
		}).catch((message) => {
			setError(message);
		});
	};

	if (!category) {
		return <>
			<br />
			<Grid container spacing={2}>
				<Grid item xs={4}>
					<Skeleton variant="circular" width={100} height={100} />
				</Grid>
				<Grid item xs={8}>
					<Skeleton variant="text" sx={{ fontSize: '1rem' }} />
				</Grid>
				<Grid item xs={12}>
					<Skeleton variant="rectangular" width={210} height={60} />
				</Grid>
				<Grid item xs={9}>
					Tiers
				</Grid>
				<Grid item xs={3}>
					Create
				</Grid>
			</Grid>
		</>;
	}

	const getTierRow = (tier: Tier) => {
		return <TableRow key={tier}>
			<TableCell
				width="10%"
				style={{ backgroundColor: tierColors[tier] }}
			>
				<h6 className="text-center">{tierNames[tier]}</h6>
			</TableCell>
			<TableCell>
				<Grid container spacing={1}>
					{items.filter(q => q.tier === +tier).map((item) => {
						return <Grid item xs={2} component="button" onClick={() => setItem(item)}>
							<Grid container spacing={1}>
								<Grid item xs={6}>
									<Avatar src={item.src} alt={item.title} />
								</Grid>
								<Grid item xs={6}>
									<Typography component="h4" variant="h4" color="primary" gutterBottom>
										{item.title}
									</Typography>
								</Grid>
							</Grid>
						</Grid>
					})}
				</Grid>
			</TableCell>
		</TableRow>;
	}

	return <>
		<br />
		<Grid container spacing={2}>
			<Grid item xs={4}>
				<Avatar sx={{ width: 100, height: 100 }} alt={category.title} src={category.src} />
			</Grid>
			<Grid item xs={8}>
				<Typography component="h4" variant="h4" color="primary" gutterBottom>
					{category.title}
				</Typography>
			</Grid>
			<Grid item xs={12}>
				<Typography variant="body1" gutterBottom>
					{category.description}
				</Typography>
			</Grid>
			<Grid item xs={9}>
				<TableContainer component={Paper}>
					<Table sx={{ minWidth: '100%' }} aria-label="simple table">
						<TableHead>
							<TableRow>
								<TableCell width="10%">Tiers</TableCell>
								<TableCell>Items</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{tiers.map((key, index) => {
								return getTierRow(key as unknown as Tier);
							})}
						</TableBody>
					</Table>
				</TableContainer>
			</Grid>
			<Grid item xs={3}>
				<Grid container spacing={2}>
					<Grid item xs={6}>
						<Avatar sx={{ width: 100, height: 100 }} alt={item.title} src={item.src} />
					</Grid>
					<Grid item xs={4}>
						<Button
							variant="contained"
							component="label"
						>
							Upload File
							<Input
								type="file"
								hidden
								id="file"
								name="file"
								autoComplete="file"
								onChange={uploadImage}
							/>
						</Button>
					</Grid>
				</Grid>
				<Box
					component="form"
					noValidate
					onSubmit={handleSubmit}
				>
					<TextField
						required
						fullWidth
						margin="normal"
						id="title"
						label="Title"
						name="title"
						autoComplete="title"
						InputLabelProps={{ shrink: true }}
						value={item.title}
						onChange={(e) => {
							setItem((prev) => {
								return {
									...prev,
									title: e.currentTarget.value
								};
							});
						}}
					/>
					<TextField
						fullWidth
						margin="normal"
						name="description"
						label="Description"
						type="description"
						id="description"
						autoComplete="description"
						InputLabelProps={{ shrink: true }}
						value={item.description}
						onChange={(e) => {
							setItem((prev) => {
								return {
									...prev,
									description: e.currentTarget.value
								};
							});
						}}
					/>
					<FormControl margin="normal" fullWidth>
						<InputLabel id="demo-simple-select-label">Tier</InputLabel>
						<Select
							labelId="demo-simple-select-label"
							id="demo-simple-select"
							value={item.tier.toString()}
							label="Tier"
							onChange={(e: SelectChangeEvent) => {
								setItem((prev) => {
									return {
										...prev,
										tier: +(e.target.value as unknown as Tier)
									};
								});
							}}
						>
							{tiers.map((tier, index) => {
								return <MenuItem
									key={index}
									className="text-center"
									style={{ backgroundColor: tierColors[tier] }}
									value={tier}
								>
									<h6>{tierNames[tier]}</h6>
								</MenuItem>
							})}
						</Select>
					</FormControl>
					<Grid container spacing={2}>
						<Grid item xs={6}>
							<Button
								type="submit"
								variant="contained"
								className="float-right"
								endIcon={<SaveIcon />}
							>
								Save
							</Button>
						</Grid>
						<Grid item xs={6}>
							<Button
								type="button"
								color="error"
								variant="contained"
								className="float-right"
								onClick={() => {
									setItem(getInitItem());
								}}
								endIcon={<ClearIcon />}
							>
								Clear
							</Button>
						</Grid>
					</Grid>
					<br />
					{error && <Alert severity="error">{error}</Alert>}
				</Box>
			</Grid>
		</Grid>
	</>;
};

export default CategoryEditor;