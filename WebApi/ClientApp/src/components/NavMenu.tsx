import * as React from 'react';
import { Container } from 'reactstrap';
import { useHistory } from 'react-router-dom';
import { AppBar, Avatar, Box, Button, IconButton, Menu, MenuItem, Toolbar, Tooltip, Typography } from '@mui/material';
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

	const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
	const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

	const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
		setAnchorElNav(event.currentTarget);
	};
	const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
		setAnchorElUser(event.currentTarget);
	};

	const handleCloseNavMenu = () => {
		setAnchorElNav(null);
	};

	const handleCloseUserMenu = () => {
		setAnchorElUser(null);
	};

	const login = userContext.login;

	return <AppBar position="static">
		<Container>
			<Toolbar disableGutters>
				<Typography
					variant="h6"
					noWrap
					component="a"
					onClick={() => login ? history.push('/dashboard') : history.push('/')}
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
				<Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
					<IconButton
						size="large"
						aria-label="account of current user"
						aria-controls="menu-appbar"
						aria-haspopup="true"
						onClick={handleOpenNavMenu}
						color="inherit"
					>
						<MenuIcon />
					</IconButton>
					<Menu
						id="menu-appbar"
						anchorEl={anchorElNav}
						anchorOrigin={{
							vertical: 'bottom',
							horizontal: 'left',
						}}
						keepMounted
						transformOrigin={{
							vertical: 'top',
							horizontal: 'left',
						}}
						open={Boolean(anchorElNav)}
						onClose={handleCloseNavMenu}
						sx={{
							display: { xs: 'block', md: 'none' },
						}}
					>
						<MenuItem onClick={handleCloseNavMenu}>
							<Typography textAlign="center">Categories</Typography>
						</MenuItem>
					</Menu>
				</Box>
				{login && <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
					<Button
						onClick={() => {
							login && history.push('/categories');
						}}
						sx={{ my: 2, color: 'white', display: 'block' }}
					>
						Categories
					</Button>
				</Box>}
				{login && <Box sx={{ flexGrow: 0 }}>
					<Tooltip title="Open settings">
						<IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
							<Avatar alt="User" src="" />
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
						<MenuItem onClick={() => { }}>
							<Typography textAlign="center"><AccountBoxIcon />&nbsp;Profile</Typography>
						</MenuItem>
						<MenuItem onClick={logout}>
							<Typography textAlign="center"><LogoutIcon />&nbsp;Logout</Typography>
						</MenuItem>
					</Menu>
				</Box>}
			</Toolbar>
		</Container>
	</AppBar>;
};

export default NavMenu;