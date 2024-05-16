import * as React from 'react';
import { Container } from 'reactstrap';
import { Link, useHistory } from 'react-router-dom';
import { Alert, AppBar, Avatar, Box, Button, Grid, IconButton, MenuItem, Toolbar, Typography } from '@mui/material';
import { clientGet, clientUpdate } from '../utils/client';
import LogoutIcon from '@mui/icons-material/Logout';
import { useUserContext } from '../utils/userContext';
import SendIcon from '@mui/icons-material/Send';

const NavMenu: React.FC<{}> = () => {
	const history = useHistory();
	const [info, setInfo] = React.useState<string | undefined>();

	const userContext = useUserContext();

	const logout = () => {
		clientGet('login').then((res) => {
			userContext.actions.removeUser();
			history.push('/');
		}).catch((message) => {

		});
	};

	const login = userContext.login;

	return <AppBar position="static">
		<Container>
			<Toolbar disableGutters>
				<Grid container spacing={1}>
					<Grid item xs>
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
					<Grid item xs={1}>
						{login && <IconButton title="Profile" onClick={() => history.push('/profile')} sx={{ p: 0 }}>
							<Avatar alt={login.userName} src={login.src} />
						</IconButton>}
					</Grid>
					<Grid item xs={2}>
						{login && <MenuItem onClick={logout}>
							<Typography textAlign="center"><LogoutIcon />&nbsp;Logout</Typography>
						</MenuItem>}
					</Grid>
				</Grid>
			</Toolbar>
		</Container>
		{login && !login.emailConfirmed && <Alert
			severity="warning"
			action={userContext.login &&  <IconButton
				aria-label="close"
				color="inherit"
				size="small"
				onClick={() => {
					clientUpdate('registration', {
						userId: userContext.login!.id,
					}).then((res) => {
						setInfo('A registration confirmation email was sent to mail.');
					});
				}}
			>
				<SendIcon fontSize="inherit" />
			</IconButton>}
		>
			{info ? info : 'Email is not confirmed'}
		</Alert>}
	</AppBar>;
};

export default NavMenu;