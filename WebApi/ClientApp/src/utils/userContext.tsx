import React, { createContext, useContext } from 'react';
import { clientGet } from './client';
import Profile from '../components/Profile';
import CircularProgress from '@mui/material/CircularProgress';

export interface UserContext {
	login: Profile | null;
	actions: {
		getUser: () => Profile | null,
		setUser: (user: Profile) => void,
		removeUser: () => void,
	}
}

const initialContext: UserContext = {
	login: null,
	actions: {
		getUser: (): Profile | null => null,
		setUser: (user: Profile) => { },
		removeUser: () => { },
	}
}

const createUserContext = createContext<UserContext>(initialContext);

export const useUserContext = () => useContext(createUserContext);

interface ComponentProps {

}

export const UserContextProvider: React.FC<ComponentProps> = ({
	children
}) => {
	const UserContext = createUserContext;

	const [login, setLogin] = React.useState<Profile | null>(null);
	const [loading, setLoading] = React.useState<boolean>(false);

	React.useEffect(() => {
		setLoading(true);
		clientGet('profile')
			.then((profile) => {
				setLogin(profile);
			})
			.catch((res) => {

			})
			.finally(() => {
				setLoading(false);
			});
	}, []);

	const value: UserContext = {
		login: login,
		actions: {
			getUser: (): Profile | null => {
				return login;
			},
			setUser: (user: Profile) => {
				setLogin(user);
			},
			removeUser: () => {
				setLogin(null);
			},
		}
	};

	return <UserContext.Provider value={value}>
		{loading ? <div className="text-center"><br /><CircularProgress /></div> : children}
	</UserContext.Provider>;
}

export default UserContextProvider;