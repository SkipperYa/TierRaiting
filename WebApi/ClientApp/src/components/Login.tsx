import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { clientPost, clientUpdate } from '../utils/client';
import { Alert, Link } from '@mui/material';
import { useHistory } from 'react-router-dom';
import { useUserContext } from '../utils/userContext';

const defaultTheme = createTheme();

export default function Login() {
	const [error, setError] = React.useState<string | undefined>();
	const [info, setInfo] = React.useState<string | undefined>();
	const [userId, setUserId] = React.useState<string>();
	const [showSendEmailConfirmed, setShowSendEmailConfirmed] = React.useState<boolean>(false);
	const history = useHistory();
	const userContext = useUserContext();

	const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		const data = new FormData(event.currentTarget);

		clientPost('login', {
			email: data.get('email'),
			password: data.get('password'),
		}).then((res) => {
			setError(undefined);
			if (res.emailConfirmed) {
				userContext.actions.setUser(res);
				setUserId(res.id);
				setShowSendEmailConfirmed(false);
				history.push('/categories');
			} else {
				setShowSendEmailConfirmed(true);
				setUserId(res.id);
				setError('Email is not confirmed.');
			}
		}).catch((message) => {
			setError(message);
		});
	};

	return (
		<ThemeProvider theme={defaultTheme}>
			<Container component="main" maxWidth="xs">
				<CssBaseline />
				<Box
					sx={{
						marginTop: 8,
						display: 'flex',
						flexDirection: 'column',
						alignItems: 'center',
					}}
				>
					<Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
						<LockOutlinedIcon />
					</Avatar>
					<Typography component="h1" variant="h5">
						Sign In
					</Typography>
					<Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
						<Grid container spacing={2}>
							<Grid item xs={12}>
								<TextField
									required
									fullWidth
									id="email"
									label="Email Address"
									name="email"
									autoComplete="email"
								/>
							</Grid>
							<Grid item xs={12}>
								<TextField
									required
									fullWidth
									name="password"
									label="Password"
									type="password"
									id="password"
									autoComplete="new-password"
								/>
							</Grid>
						</Grid>
						<Button
							type="submit"
							fullWidth
							variant="contained"
							sx={{ mt: 3, mb: 2 }}
						>
							Sign In
						</Button>
						{info && <Alert severity="info">{info}</Alert>}
						{error && <Alert severity="error">{error}</Alert>}
						<Grid container justifyContent="flex-end">
							<Grid item>
								<Link href="/registration" variant="body2">
									Sign Up
								</Link>
							</Grid>
						</Grid>
						{showSendEmailConfirmed && <Grid container justifyContent="flex-end">
							<Grid item>
								<Button
									type="button"
									fullWidth
									variant="contained"
									sx={{ mt: 3, mb: 2 }}
									onClick={() => {
										clientUpdate('registration', {
											userId: userId,
										}).then((res) => {
											setInfo('A registration confirmation email was sent to mail.');
										});
									}}
								>
									Send confirmation email
								</Button>
							</Grid>
						</Grid>}
					</Box>
				</Box>
			</Container>
		</ThemeProvider>
	);
}