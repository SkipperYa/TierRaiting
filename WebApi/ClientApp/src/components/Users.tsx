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
import { User } from '../objects/User';

interface ComponentProps {

}

const Users: React.FC<ComponentProps> = ({

}) => {
	const [users, setUsers] = React.useState<Array<User> | undefined>(undefined);
	const [pagination, setPagination] = React.useState({ page: 1, total: 0 });
	const [text, setText] = React.useState<string | undefined>();
	const [open, setOpen] = React.useState<boolean>(false);

	const history = useHistory();

	const getTextQuery = () => text ? `&text=${text}` : '';

	const loadUsers = () => {
		setUsers(undefined);
		clientGet(`users?page=${pagination.page}${getTextQuery()}`)
			.then((res) => {
				setUsers(res.list as Array<User>);
				setPagination((prev) => {
					return { ...prev, total: res.total };
				});
			});
	}

	React.useEffect(() => {
		loadUsers();
	}, [pagination.page]);

	return <div>
		<br />
		<Grid container spacing={1}>
			<Grid item xs={4}>
				<Typography component="h4" variant="h4" color="primary" gutterBottom>
					Users #{pagination.total}
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
					onClick={() => loadUsers()}
				>
					<SearchIcon />
				</IconButton>
			</Grid>
		</Grid>
		<br/>
		<Divider />
		{!users
			? <Skeleton width={1150} height={500} variant="rectangular" animation="wave" />
			: <>
				<TableContainer component={Paper}>
					<Table sx={{ minWidth: '100%' }} aria-label="simple table">
						<TableHead>
							<TableRow>
								<TableCell width="10%"></TableCell>
								<TableCell>User Name</TableCell>
								<TableCell width="15%"></TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{users.map((user, index) => <TableRow key={user.id}>
								<TableCell width="10%"><Avatar src={user.src} alt={user.userName} /></TableCell>
								<TableCell width="20%">{user.userName}</TableCell>
								<TableCell>
									<Grid container spacing={1}>
										<Grid item xs={3}>
											<IconButton
												title="Go to..."
												color="success"
												onClick={() => {
													/*history.push({
														pathname: 'category',
														search: `id=${category.id}`
													});*/
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
													/*setCategory(category);
													setOpen(true);*/
												}}
											>
												<CreateIcon />
											</IconButton>
										</Grid>
										{/*<Grid item xs={3}>
											<IconButton
												title="Delete Category"
												color="error"
												disabled={category.itemsCount > 0}
												onClick={() => handleDelete(category.id)}
											>
												<DeleteIcon />
											</IconButton>
										</Grid>*/}
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

export default Users;