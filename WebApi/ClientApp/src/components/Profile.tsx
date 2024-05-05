import React, { ChangeEvent } from 'react';
import { User } from '../objects/User';
import { clientGet, clientUpdate, clientUpload } from '../utils/client';
import { Alert, Avatar, Button, Grid, Input, TextField, Typography } from '@mui/material';
import SaveIcon from '@mui/icons-material/Save';
import { useUserContext } from '../utils/userContext';

interface ComponentProps {

}

interface Profile extends User {
	email: string;
}

const Profile: React.FC<ComponentProps> = ({

}) => {
	const [user, setUser] = React.useState<Profile>();
	const [error, setError] = React.useState<string | undefined>();

	const userContext = useUserContext();

	React.useEffect(() => {
		clientGet('profile').then((res) => {
			setUser(res);
		})
	}, []);

	if (!user) {
		return <>Loading...</>
	}

	const saveUser = () => {
		clientUpdate('profile', {
			...user
		}).then((res) => {
			setUser(res);
			userContext.actions.setUser(res);
		}).catch((message) => {
			setError(message);
		});
	}

	const uploadImage = (event: ChangeEvent<HTMLInputElement>) => {
		event.preventDefault();

		if (!event.target.files) {
			return;
		}

		clientUpload('image', event.target.files[0]).then((res) => {
			setUser((prev) => {
				return {
					...prev!,
					src: res.src
				};
			});
		}).catch((message) => {
			setError(message);
		});
	};

	return <>
		<br />
		<Grid container spacing={2}>
			<Grid item xs={6}>
				<Typography component="h4" variant="h4" color="primary" gutterBottom>
					Profile
				</Typography>
			</Grid>
			<Grid item xs={6}>
				<Button
					type="submit"
					variant="contained"
					className="float-left"
					color="success"
					endIcon={<SaveIcon />}
					onClick={saveUser}
				>
					Save
				</Button>
			</Grid>
			<Grid item xs={6}>
				<Avatar sx={{ width: 150, height: 150 }} alt={user.userName} src={user.src} />
			</Grid>
			<Grid item xs={6}>
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
						onChange={uploadImage}
					/>
				</Button>
			</Grid>
			<Grid item xs={6}>
				<TextField
					fullWidth
					margin="none"
					name="email"
					label="Email"
					type="text"
					id="email"
					autoComplete="Email"
					InputLabelProps={{ shrink: true }}
					value={user!.email}
					onChange={(e) => {
						setUser((prev) => {
							return {
								...prev!,
								email: e.currentTarget.value,
							};
						});
					}}
				/>
			</Grid>
			<Grid item xs={6}>
				<TextField
					fullWidth
					margin="none"
					name="userName"
					label="User Name"
					type="text"
					id="userName"
					autoComplete="User Name"
					InputLabelProps={{ shrink: true }}
					value={user!.userName}
					onChange={(e) => {
						setUser((prev) => {
							return {
								...prev!,
								userName: e.currentTarget.value,
							};
						});
					}}
				/>
			</Grid>
			<Grid item xs={6}>
				{error && <Alert severity="error">{error}</Alert>}
			</Grid>
		</Grid>
	</>;
}

export default Profile;