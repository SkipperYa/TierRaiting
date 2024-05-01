import React, { createContext, useContext } from 'react';
import { User } from '../objects/User';

export interface UserContext {
	login: User | null;
	actions: {
		getUser: () => User | null,
		setUser: (user: User) => void,
		removeUser: () => void,
	}
}

const initialContext: UserContext = {
	login: null,
	actions: {
		getUser: (): User | null => null,
		setUser: (user: User) => { },
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

	const getInitilUser = () => {
		const item = localStorage.getItem('user');
		return item ? JSON.parse(item) as User : null;
	};

	const [login, setLogin] = React.useState<User | null>(getInitilUser());

	const value: UserContext = {
		login: login,
		actions: {
			getUser: (): User | null => {
				return login;
			},
			setUser: (user: User) => {
				localStorage.setItem('user', JSON.stringify(user));
				setLogin(user);
			},
			removeUser: () => {
				localStorage.removeItem('user');
				setLogin(null);
			},
		}
	};

	return <UserContext.Provider value={value}>
		{children}
	</UserContext.Provider>;
}

export default UserContextProvider;