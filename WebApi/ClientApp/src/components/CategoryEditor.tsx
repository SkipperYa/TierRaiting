import React, { ChangeEvent } from 'react';
import { Category, CategotyTypeNames } from '../objects/Category';
import { useLocation } from 'react-router-dom';
import { clientDelete, clientGet, clientPost, clientUpdate, clientUpload } from '../utils/client';
import { Alert, Autocomplete, Avatar, Box, Button, Card, CardActions, CardContent, Chip, FormControl, Grid, IconButton, Input, InputLabel, MenuItem, Paper, Select, SelectChangeEvent, Skeleton, Tab, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Tabs, TextField, Tooltip, Typography } from '@mui/material';
import { Item, tierColors, tierNames } from '../objects/Item';
import { Tier } from '../objects/enums';
import ClearIcon from '@mui/icons-material/Clear';
import SaveIcon from '@mui/icons-material/Save';
import DeleteIcon from '@mui/icons-material/Delete';
import CreateIcon from '@mui/icons-material/Create';

interface ItemOption {
	imgSrc: string;
	name: string;
}

export interface ComponentProps {

}
// https://openlibrary.org/dev/docs/api/search
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
	const [options, setOptions] = React.useState<Array<ItemOption>>([]);
	const [loading, setLoading] = React.useState<boolean>(false);
	const [inputValue, setInputValue] = React.useState<string>('');
	const checkTimer = React.useRef<any>(null);

	const handleDelete = (id: string) => {
		clientDelete('item', {
			id: id,
		}).then((res) => {
			setError(undefined);
			setItem(getInitItem());
			loadItems();
		}).catch((message) => {
			setError(message);
		});
	};

	const loadItems = () => {
		clientGet(`items?categoryId=${categoryId}`)
			.then((res) => {
				setItems(res.list);
			});
	};

	React.useEffect(() => {
		if (!inputValue || !category) {
			return;
		}

		if (checkTimer.current) {
			clearTimeout(checkTimer.current);
		}

		setLoading(true);

		checkTimer.current = setTimeout(() => {
			clientGet(`itemOptions?text=${inputValue}&categoryType=${category.categoryType}`)
				.then((res) => {
					setOptions(res.list);
				})
				.finally(() => setLoading(false))
		}, 1000, [inputValue, category.categoryType]);
	}, [inputValue]);

	React.useEffect(() => {
		clientGet(`category?id=${categoryId}`)
			.then((res) => {
				setCategory(res);
				setItems(res.items ? res.items : []);
			});
	}, []);

	const tiers = (Object.keys(Tier) as (keyof typeof Tier)[])
		.filter((q) => +q >= 0)
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
			loadItems();
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
						return <Grid item xs={2}>
							<Card raised variant="outlined" sx={{ alignItems: 'center' }}>
								<CardContent>
									<Avatar src={item.src} alt={item.title} />
									<Tooltip placement="right" title={item.title}>
										<Typography
											noWrap
											sx={{ fontSize: 18 }}
											color="text.primary"
											gutterBottom
										>
											{item.title}
										</Typography>
									</Tooltip>
									<Tooltip placement="right" title={item.description}>
										<Typography
											noWrap
											sx={{ fontSize: 14 }}
											color="text.secondary"
											gutterBottom
										>
											{item.description}
										</Typography>
									</Tooltip>
								</CardContent>
								<CardActions>
									<IconButton size="small" onClick={() => setItem(item)}><CreateIcon /></IconButton>
									<IconButton size="small" onClick={() => handleDelete(item.id)}><DeleteIcon /></IconButton>
								</CardActions>
							</Card>
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
					{category.title} <Chip label={CategotyTypeNames[category.categoryType]} color="primary" />
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
					{category.categoryType > 0 && <Grid item xs={12}>
						<div>
							<br />
							<Autocomplete
								loading={loading}
								onClose={() => setOptions([])}
								onChange={(event: any, newValue: ItemOption | null) => {
									setItem({
										...getInitItem(),
										title: newValue ? newValue.name : '',
										src: newValue ? newValue.imgSrc : '',
									});
								}}
								inputValue={inputValue}
								onInputChange={(event, newInputValue) => {
									setInputValue(newInputValue);
								}}
								id="controllable-states-demo"
								options={options}
								getOptionLabel={(option: ItemOption) => option.name}
								sx={{ width: 300 }}
								renderInput={(params) => <TextField {...params} label={CategotyTypeNames[category.categoryType]} />}
								renderOption={(props, option) => (
									<Box component="li" sx={{ '& > img': { mr: 2, flexShrink: 0 } }} {...props}>
										<Avatar sx={{ width: 50, height: 50, marginRight: '5px' }} alt={option.name} src={option.imgSrc} />
										{option.name}
									</Box>
								)}
							/>
						</div>
					</Grid>}
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
									style={{ backgroundColor: tierColors[tier], alignContent: 'center' }}
									value={tier}
								>
									<h6 style={{ alignContent: 'center' }}>{tierNames[tier]}</h6>
								</MenuItem>
							})}
						</Select>
					</FormControl>
					<Grid container spacing={2}>
						<Grid item xs={4}>
							<IconButton
								type="submit"
								title="Save"
								color="primary"
							>
								<SaveIcon />
							</IconButton>
						</Grid>
						<Grid item xs={4}>
							<IconButton
								title="Clear"
								color="error"
								onClick={() => setItem(getInitItem())}
							>
								<ClearIcon />
							</IconButton>
						</Grid>
						{/*<Grid item xs={4}>
							<IconButton
								title="Delete Item"
								color="error"
								disabled={!item.id}
								onClick={() => handleDelete(item.id)}
							>
								<DeleteIcon />
							</IconButton>
						</Grid>*/}
					</Grid>
					<br />
					{error && <Alert severity="error">{error}</Alert>}
				</Box>
			</Grid>
		</Grid>
	</>;
};

export default CategoryEditor;