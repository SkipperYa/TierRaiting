import { Avatar, Button, Grid, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableHead, TablePagination, TableRow, Typography } from '@mui/material';
import React from 'react';
import { clientGet } from '../utils/client';
import { Category } from '../objects/Category';
import AddIcon from '@mui/icons-material/Add';
import CreateIcon from '@mui/icons-material/Create';
import ArrowRightAltIcon from '@mui/icons-material/ArrowRightAlt';
import DeleteIcon from '@mui/icons-material/Delete';

interface ComponentProps {

}

const Categories: React.FC<ComponentProps> = ({

}) => {
	const [categories, setCategories] = React.useState<Category[] | undefined>(undefined);
	const [pagination, setPagination] = React.useState({ page: 1, total: 0 });

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
	}, [JSON.stringify(pagination)]);

	if (!categories) {
		return <><br /><div className="text-center">Loading...</div></>;
	}

	return <div>
		<br/>
		<Grid container spacing={2}>
			<Grid item xs={11}>
				<Typography component="h4" variant="h4" color="primary" gutterBottom>
					Categories
				</Typography>
			</Grid>
			<Grid item xs={1}>
				<Button title="Add new Category" variant="contained" className="btn btn-primary"><AddIcon /></Button>
			</Grid>
		</Grid>
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
										className="btn btn-success"
									>
										<ArrowRightAltIcon />
									</Button>
								</Grid>
								<Grid item xs={2}>
									<Button
										title="Edit Category"
										variant="contained"
										className="btn btn-primary"
									>
										<CreateIcon />
									</Button>
								</Grid>
								<Grid item xs={2}>
									<Button
										title="Edit Category"
										variant="contained"
										className="btn btn-danger"
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
				count={pagination.total > 10 ? pagination.total / 10 : 1}
				onChange={(event: any, page: number) => {
					setPagination((prev) => {
						return { ...prev, total: page };
					});
				}}
			/>
		</div>
	</div>;
};

export default Categories;