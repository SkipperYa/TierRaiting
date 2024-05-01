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

export default function Registration() {
	const [error, setError] = React.useState<string | undefined>();
	const [message, setMessage] = React.useState<string | undefined>();

	const history = useHistory();
	const userContext = useUserContext();

	const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		const data = new FormData(event.currentTarget);

		clientPost('registration', {
			email: data.get('email'),
			userName: data.get('userName'),
			password: data.get('password'),
			passwordConfirm: data.get('passwordConfirm'),
		}).then((res) => {
			setError(undefined);
			userContext.actions.setUser(res);
			setMessage('A registration confirmation email was sent to mail.')
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
						Sign up
					</Typography>
					<Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
						<Grid container spacing={2}>
							<Grid item xs={12}>
								<TextField
									required
									fullWidth
									id="userName"
									label="User Name"
									name="userName"
									autoComplete="userName"
								/>
							</Grid>
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
							<Grid item xs={12}>
								<TextField
									required
									fullWidth
									name="passwordConfirm"
									label="Confirm Password"
									type="password"
									id="passwordConfirm"
									autoComplete="new-password-confirm"
								/>
							</Grid>
						</Grid>
						<Button
							type="submit"
							fullWidth
							variant="contained"
							sx={{ mt: 3, mb: 2 }}
						>
							Sign Up
						</Button>
						{error && <Alert severity="error">{error}</Alert>}
						<Grid container justifyContent="flex-end">
							<Grid item>
								<Link href="/" variant="body2">
									Already have an account? Sign in
								</Link>
							</Grid>
						</Grid>
					</Box>
				</Box>
			</Container>
		</ThemeProvider>
	);
}