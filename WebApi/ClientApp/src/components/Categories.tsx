import { Alert, Avatar, Box, Button, Container, Divider, Grid, Input, Pagination, Table, TableBody, TableCell, TableHead, TableRow, TextField, Typography } from '@mui/material';
import React from 'react';
import { clientGet, clientPost } from '../utils/client';
import { Category } from '../objects/Category';
import AddIcon from '@mui/icons-material/Add';
import CreateIcon from '@mui/icons-material/Create';
import ArrowRightAltIcon from '@mui/icons-material/ArrowRightAlt';
import DeleteIcon from '@mui/icons-material/Delete';

interface ComponentProps {

}

const Categories: React.FC<ComponentProps> = ({

}) => {
	const [categories, setCategories] = React.useState<Array<Category> | undefined>(undefined);
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
		clientPost('category', {
			title: data.get('title'),
			description: data.get('description'),
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
						/>
					</Grid>
					<Grid item md={8} xs={2}>
						<Button
							type="submit"
							fullWidth
							variant="contained"
							sx={{ mt: 3, mb: 2 }}
							className="float-right"
						>
							<AddIcon /> Create
						</Button>
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
		<Table>
			<TableHead>
				<TableRow>
					<TableCell width="10%"></TableCell>
					<TableCell>Title</TableCell>
					<TableCell>Description</TableCell>
					<TableCell>#</TableCell>
					<TableCell>Actions</TableCell>
				</TableRow>
			</TableHead>
			<TableBody>
				{categories.map((category, index) => {
					return <TableRow key={index}>
						<TableCell width="10%" key={category.id}><Avatar src="/static/images/avatar/1.jpg" alt={category.title} /></TableCell>
						<TableCell key={category.id}>{category.title}</TableCell>
						<TableCell key={category.id}>{category.description}</TableCell>
						<TableCell key={category.id}>{category.itemsCount}</TableCell>
						<TableCell key={category.id}>
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
									>
										<CreateIcon />
									</Button>
								</Grid>
								<Grid item xs={2}>
									<Button
										title="Edit Category"
										variant="contained"
										color="error"
									>
										<DeleteIcon />
									</Button>
								</Grid>
							</Grid>
						</TableCell>
					</TableRow>;
				})}
			</TableBody>
		</Table>
		<div style={{ marginTop: 15 }} className="float-right">
			<Pagination
				count={pagination.total > 5 ? Math.floor(pagination.total / 5) : 1}
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