import { Alert, Avatar, Box, Button, Divider, Grid, Input, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, Typography } from '@mui/material';
import React from 'react';
import { clientDelete, clientGet, clientPost, clientUpdate } from '../utils/client';
import { Category } from '../objects/Category';
import AddIcon from '@mui/icons-material/Add';
import CreateIcon from '@mui/icons-material/Create';
import ArrowRightAltIcon from '@mui/icons-material/ArrowRightAlt';
import DeleteIcon from '@mui/icons-material/Delete';
import ClearIcon from '@mui/icons-material/Clear';
import SaveIcon from '@mui/icons-material/Save';

interface ComponentProps {

}

const Categories: React.FC<ComponentProps> = ({

}) => {
	const [categories, setCategories] = React.useState<Array<Category> | undefined>(undefined);
	const [editCategory, setEditCategory] = React.useState<Category | undefined>(undefined);
	const [pagination, setPagination] = React.useState({ page: 1, total: 0 });
	const [error, setError] = React.useState<string | undefined>();

	const loadCategories = () => {
		clientGet(`categories?page=${pagination.page}`)
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
		if (editCategory) {
			clientUpdate('category', {
				id: editCategory.id,
				title: data.get('title'),
				description: data.get('description'),
			}).then((res) => {
				setError(undefined);
				setEditCategory(undefined);
				loadCategories();
			}).catch((message) => {
				setError(message);
			});
		} else {
			clientPost('category', {
				title: data.get('title'),
				description: data.get('description'),
			}).then((res) => {
				setError(undefined);
				setEditCategory(undefined);
				loadCategories();
			}).catch((message) => {
				setError(message);
			});
		}
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

	if (!categories) {
		return <><br /><div className="text-center">Loading...</div></>;
	}

	return <div>
		<br />
		<Box
			component="form"
			noValidate
			onSubmit={handleSubmit}
		>
			<Grid container spacing={2}>
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
						/>
					</Button>
				</Grid>
				<Grid item xs={8}>
					<Grid item xs={8}>
						<TextField
							required
							fullWidth
							margin="normal"
							id="title"
							label="Title"
							name="title"
							autoComplete="title"
							InputLabelProps={{ shrink: true }}
							// defaultValue={editCategory ? editCategory.title : ''}
							onChange={(e) => {
								if (editCategory) {
									setEditCategory((prev) => {
										if (!prev) {
											return undefined;
										}
										return {
											...prev,
											title: e.currentTarget.value
										};
									});
								}
							}}
						/>
					</Grid>
					<Grid item xs={8}>
						<TextField
							fullWidth
							margin="normal"
							name="description"
							label="Description"
							type="description"
							id="description"
							autoComplete="description"
							InputLabelProps={{ shrink: true }}
							defaultValue={editCategory ? editCategory.description : ''}
							/*onChange={(e) => {
								if (editCategory) {
									setEditCategory((prev) => {
										if (!prev) {
											return undefined;
										}
										return {
											...prev,
											description: e.currentTarget.value
										};
									});
								}
							}}*/
						/>
					</Grid>
					<Grid container spacing={2}>
						<Grid item xs={4}>
							<Button
								type="submit"
								fullWidth
								variant="contained"
								sx={{ mt: 3, mb: 2 }}
								className="float-right"
							>
								<SaveIcon />&nbsp;Save
							</Button>
						</Grid>
						<Grid item xs={4}>
							<Button
								type="button"
								fullWidth
								color="error"
								variant="contained"
								sx={{ mt: 3, mb: 2 }}
								className="float-right"
								disabled={!Boolean(editCategory)}
								onClick={() => setEditCategory(undefined)}
							>
								<ClearIcon />&nbsp;Clear
							</Button>
						</Grid>
						{error && <Alert severity="error">{error}</Alert>}
					</Grid>
				</Grid>
			</Grid>
		</Box>
		<Grid container spacing={2}>
			<Grid item xs={11}>
				<Typography component="h4" variant="h4" color="primary" gutterBottom>
					Categories
				</Typography>
			</Grid>
		</Grid>
		<Divider />
		<TableContainer component={Paper}>
			<Table sx={{ minWidth: 650 }} aria-label="simple table">
				<TableHead>
					<TableRow>
						<TableCell width="10%"></TableCell>
						<TableCell>Title</TableCell>
						<TableCell>Description</TableCell>
						<TableCell>#</TableCell>
						<TableCell></TableCell>
					</TableRow>
				</TableHead>
				<TableBody>
					{categories.map((category, index) => <TableRow key={category.id}>
						<TableCell width="10%"><Avatar src="/static/images/avatar/1.jpg" alt={category.title} /></TableCell>
						<TableCell width="20%">{category.title}</TableCell>
						<TableCell width="20%">{category.description}</TableCell>
						<TableCell width="10%">{category.itemsCount}</TableCell>
						<TableCell>
							<Grid container spacing={1}>
								<Grid item xs={2}>
									<Button
										title="Go to..."
										variant="contained"
										color="success"
									>
										<ArrowRightAltIcon />
									</Button>
								</Grid>
								<Grid item xs={2}>
									<Button
										title="Edit Category"
										variant="contained"
										color="secondary"
										onClick={() => setEditCategory(category)}
										disabled={Boolean(editCategory) && editCategory!.id === category.id}
									>
										<CreateIcon />
									</Button>
								</Grid>
								<Grid item xs={2}>
									<Button
										title="Delete Category"
										variant="contained"
										color="error"
										disabled={category.itemsCount > 0}
										onClick={() => handleDelete(category.id)}
									>
										<DeleteIcon />
									</Button>
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
				onChange={(event: any, page: number) => {
					setPagination((prev) => {
						return { ...prev, page: page };
					});
				}}
			/>
		</div>
	</div>;
};

export default Categories;