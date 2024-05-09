import * as React from 'react';
import { Container } from 'reactstrap';
import { useHistory } from 'react-router-dom';
import { Alert, AppBar, Avatar, Box, Button, Grid, IconButton, Menu, MenuItem, Toolbar, Tooltip, Typography } from '@mui/material';
import { clientGet } from '../utils/client';
import LogoutIcon from '@mui/icons-material/Logout';
import MenuIcon from '@mui/icons-material/Menu';
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import { useUserContext } from '../utils/userContext';

const NavMenu: React.FC<{}> = () => {
	const history = useHistory();

	const userContext = useUserContext();

	const logout = () => {
		clientGet('login').then((res) => {
			userContext.actions.removeUser();
			history.push('/');
		}).catch((message) => {

		});
	};

	const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

	const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
		setAnchorElUser(event.currentTarget);
	};

	const handleCloseUserMenu = () => {
		setAnchorElUser(null);
	};

	const login = userContext.login;

	return <AppBar position="static">
		<Container>
			<Toolbar disableGutters>
				<Grid container spacing={1}>
					<Grid item xs={9}>
						<Typography
							variant="h6"
							noWrap
							component="a"
							onClick={() => login ? history.push('/categories') : history.push('/')}
							sx={{
								mr: 2,
								display: { xs: 'none', md: 'flex' },
								fontFamily: 'monospace',
								fontWeight: 700,
								letterSpacing: '.3rem',
								color: 'inherit',
								textDecoration: 'none',
								cursor: 'pointer'
							}}
						>
							Tier Rating
						</Typography>
					</Grid>
					<Grid item xs={3}>
						{login && <Box alignContent="flex-end" sx={{ flexGrow: 0 }}>
							<Tooltip title="Open settings">
								<IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
									<Avatar alt={login.userName} src={login.src} />
								</IconButton>
							</Tooltip>
							<Menu
								sx={{ mt: '45px' }}
								id="menu-appbar"
								anchorEl={anchorElUser}
								anchorOrigin={{
									vertical: 'top',
									horizontal: 'right',
								}}
								keepMounted
								transformOrigin={{
									vertical: 'top',
									horizontal: 'right',
								}}
								open={Boolean(anchorElUser)}
								onClose={handleCloseUserMenu}
							>
								<MenuItem onClick={() => history.push('/profile')}>
									<Typography textAlign="center"><AccountBoxIcon />&nbsp;Profile</Typography>
								</MenuItem>
								<MenuItem onClick={logout}>
									<Typography textAlign="center"><LogoutIcon />&nbsp;Logout</Typography>
								</MenuItem>
							</Menu>
						</Box>}
					</Grid>
				</Grid>
			</Toolbar>
		</Container>
		{login && !login.emailConfirmed && <Alert severity="warning">Email is not confirmed.</Alert>}
	</AppBar>;
};

export default NavMenu;