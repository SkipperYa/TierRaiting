import * as React from 'react';
import { Container } from 'reactstrap';
import { useHistory } from 'react-router-dom';
import { AppBar, Avatar, Box, Button, IconButton, Menu, MenuItem, Toolbar, Tooltip, Typography } from '@mui/material';
import { clientGet, getUser, removeUser } from '../utils/client';
import LogoutIcon from '@mui/icons-material/Logout';
import MenuIcon from '@mui/icons-material/Menu';
import AccountBoxIcon from '@mui/icons-material/AccountBox';

const NavMenu: React.FC<{}> = () => {
	const history = useHistory();

	const logout = () => {
		clientGet('login').then((res) => {
			removeUser();
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

	return <AppBar position="static">
		<Container maxWidth="xl">
			<Toolbar disableGutters>
				<Typography
					variant="h6"
					noWrap
					component="a"
					onClick={() => history.push('/')}
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
					Tier Raiting
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
				<Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
					<Button
						onClick={() => history.push('/fetch-data/1')}
						sx={{ my: 2, color: 'white', display: 'block' }}
					>
						Categories
					</Button>
				</Box>
				<Box sx={{ flexGrow: 0 }}>
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
				</Box>
			</Toolbar>
		</Container>
	</AppBar>;
};

export default NavMenu;