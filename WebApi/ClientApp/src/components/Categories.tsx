import { Alert, Avatar, Box, Button, Chip, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Divider, Grid, IconButton, Input, InputLabel, MenuItem, Modal, Pagination, Paper, Select, SelectChangeEvent, Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, Typography } from '@mui/material';
import React, { ChangeEvent } from 'react';
import { clientDelete, clientGet, clientPost, clientUpdate, clientUpload } from '../utils/client';
import { Category, CategotyTypeNames } from '../objects/Category';
import CreateIcon from '@mui/icons-material/Create';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import DeleteIcon from '@mui/icons-material/Delete';
import ClearIcon from '@mui/icons-material/Clear';
import SaveIcon from '@mui/icons-material/Save';
import AddIcon from '@mui/icons-material/Add';
import SearchIcon from '@mui/icons-material/Search';
import { useHistory } from 'react-router-dom';
import { CategoryType } from '../objects/enums';

interface ComponentProps {

}

const initCategory: Category = {
	id: '',
	title: '',
	description: '',
	items: [],
	itemsCount: 0,
	src: '',
	categoryType: CategoryType.None,
}

const Categories: React.FC<ComponentProps> = ({

}) => {
	const [categories, setCategories] = React.useState<Array<Category> | undefined>(undefined);
	const [category, setCategory] = React.useState<Category>(initCategory);
	const [pagination, setPagination] = React.useState({ page: 1, total: 0 });
	const [error, setError] = React.useState<string | undefined>();
	const [text, setText] = React.useState<string | undefined>();
	const [open, setOpen] = React.useState<boolean>(false);

	const history = useHistory();

	const getTextQuery = () => text ? `&text=${text}` : '';

	const loadCategories = () => {
		setCategories(undefined);
		clientGet(`categories?page=${pagination.page}${getTextQuery()}`)
			.then((res) => {
				setCategories(res.list as Array<Category>);
				setPagination((prev) => {
					return { ...prev, total: res.total };
				});
			});
	}

	React.useEffect(() => {
		loadCategories();
	}, [pagination.page]);

	const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		const data = new FormData(event.currentTarget);
		(category.id ? clientUpdate('category', {
			id: category.id,
			title: data.get('title'),
			description: data.get('description'),
			src: category.src,
			categoryType: category.categoryType,
		}) : clientPost('category', {
			title: data.get('title'),
			description: data.get('description'),
			src: category.src,
			categoryType: category.categoryType,
		})).then((res) => {
			setError(undefined);
			setCategory(initCategory);
			setOpen(false);
			loadCategories();
		}).catch((message) => {
			setError(message);
		});
	};

	const uploadImage = (event: ChangeEvent<HTMLInputElement>) => {
		event.preventDefault();

		if (!event.target.files) {
			return;
		}

		clientUpload('image', event.target.files[0]).then((res) => {
			setCategory((prev) => {
				return {
					...prev,
					src: res.src
				};
			});
		}).catch((message) => {
			setError(message);
		});
	};

	const handleDelete = (id: string) => {
		clientDelete('category', {
			id: id,
		}).then((res) => {
			setError(undefined);
			loadCategories();
		}).catch((message) => {
			setError(message);
		});
	};

	return <div>
		<Dialog
			open={open}
			aria-labelledby="alert-dialog-title"
			aria-describedby="alert-dialog-description"
		>
			<DialogTitle id="alert-dialog-title">
				{category.id ? 'Edit Category' : 'Create Category'}
			</DialogTitle>
			<DialogContent>
				<DialogContent id="alert-dialog-description">
					<Grid container spacing={2}>
						<Grid item xs={6}>
							<Avatar sx={{ width: 100, height: 100 }} alt={category.title} src={category.src} />
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
							value={category.title}
							onChange={(e) => {
								setCategory((prev) => {
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
							value={category.description}
							onChange={(e) => {
								setCategory((prev) => {
									return {
										...prev,
										description: e.currentTarget.value
									};
								});
							}}
						/>
						<InputLabel id="demo-simple-select-label">Type</InputLabel>
						<Select
							labelId="demo-simple-select-label"
							id="demo-simple-select"
							value={category.categoryType.toString()}
							label="Type"
							onChange={(event: SelectChangeEvent) => {
								setCategory((prev) => {
									return {
										...prev,
										categoryType: +event.target.value
									};
								});
							}}
						>
							<MenuItem value={CategoryType.None}>{CategotyTypeNames[CategoryType.None]}</MenuItem>
							<MenuItem value={CategoryType.Games}>{CategotyTypeNames[CategoryType.Games]}</MenuItem>
							<MenuItem value={CategoryType.Books}>{CategotyTypeNames[CategoryType.Books]}</MenuItem>
							<MenuItem value={CategoryType.Films}>{CategotyTypeNames[CategoryType.Films]}</MenuItem>
						</Select>
						<DialogActions>
							<Button
								type="submit"
								variant="contained"
								className="float-right"
								endIcon={<SaveIcon />}
							>
								Save
							</Button>
							<Button
								type="button"
								color="error"
								variant="contained"
								className="float-right"
								onClick={() => {
									setCategory(initCategory);
									setOpen(false);
								}}
								endIcon={<ClearIcon />}
							>
								Close
							</Button>
						</DialogActions>
						{error && <Alert severity="error">{error}</Alert>}
					</Box>
				</DialogContent>
			</DialogContent>
		</Dialog>
		<br />
		<Grid container spacing={1}>
			<Grid item xs={4}>
				<Typography component="h4" variant="h4" color="primary" gutterBottom>
					Categories #{pagination.total}
				</Typography>
			</Grid>
			<Grid item xs={3}>
				<TextField
					fullWidth
					margin="none"
					name="text"
					label="Text search"
					type="text"
					id="text"
					autoComplete="Search..."
					InputLabelProps={{ shrink: true }}
					value={text}
					onChange={(e) => {
						setText(e.currentTarget.value);
					}}
				/>
			</Grid>
			<Grid item xs={2}>
				<IconButton
					style={{ marginTop: '5px' }}
					title="Search"
					color="primary"
					onClick={() => loadCategories()}
				>
					<SearchIcon />
				</IconButton>
			</Grid>
			<Grid item xs={3}>
				<div className="float-right">
					<Button
						className=""
						type="submit"
						variant="contained"
						onClick={() => setOpen(true)}
						endIcon={<AddIcon />}
					>
						Create
					</Button>
				</div>
			</Grid>
		</Grid>
		<br/>
		<Divider />
		{!categories
			? <Skeleton width={1150} height={500} variant="rectangular" animation="wave" />
			: <>
				<TableContainer component={Paper}>
					<Table sx={{ minWidth: '100%' }} aria-label="simple table">
						<TableHead>
							<TableRow>
								<TableCell width="10%"></TableCell>
								<TableCell>Title</TableCell>
								<TableCell>Description</TableCell>
								<TableCell>Type</TableCell>
								<TableCell width="10%">#</TableCell>
								<TableCell width="15%"></TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{categories.map((category, index) => <TableRow key={category.id}>
								<TableCell width="10%"><Avatar src={category.src} alt={category.title} /></TableCell>
								<TableCell width="20%">{category.title}</TableCell>
								<TableCell width="20%">{category.description}</TableCell>
								<TableCell width="20%"><Chip label={CategotyTypeNames[category.categoryType]} color="primary" /></TableCell>
								<TableCell width="10%">{category.itemsCount}</TableCell>
								<TableCell>
									<Grid container spacing={1}>
										<Grid item xs={3}>
											<IconButton
												title="Go to..."
												color="success"
												onClick={() => {
													history.push({
														pathname: 'category',
														search: `id=${category.id}`
													});
												}}
											>
												<ArrowForwardIosIcon />
											</IconButton>
										</Grid>
										<Grid item xs={3}>
											<IconButton
												title="Edit Category"
												color="secondary"
												onClick={() => {
													setCategory(category);
													setOpen(true);
												}}
											>
												<CreateIcon />
											</IconButton>
										</Grid>
										<Grid item xs={3}>
											<IconButton
												title="Delete Category"
												color="error"
												disabled={category.itemsCount > 0}
												onClick={() => handleDelete(category.id)}
											>
												<DeleteIcon />
											</IconButton>
										</Grid>
									</Grid>
								</TableCell>
							</TableRow>)}
						</TableBody>
					</Table>
				</TableContainer>
				<div style={{ marginTop: 15 }} className="float-right">
					<Pagination
						count={pagination.total > 5 ? Math.ceil(pagination.total / 5) : 1}
						page={pagination.page}
						onChange={(event: any, page: number) => {
							setPagination((prev) => {
								return { ...prev, page: page };
							});
						}}
					/>
				</div>
			</>}
	</div>;
};

export default Categories;