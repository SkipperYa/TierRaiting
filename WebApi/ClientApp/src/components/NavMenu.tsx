import * as React from 'react';
import { Container, Navbar, NavbarBrand, NavItem, NavLink } from 'reactstrap';
import { Link, useHistory } from 'react-router-dom';
import './NavMenu.css';
import { Button } from '@mui/material';
import { clientGet, getUser, removeUser } from '../utils/client';
import LogoutIcon from '@mui/icons-material/Logout';

const NavMenu: React.FC<{}> = () => {
	const history = useHistory();

	const logout = () => {
		clientGet('login').then((res) => {
			removeUser();
			history.push('/');
		}).catch((message) => {

		});
	};

	return <header>
		<Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
			<Container>
				<NavbarBrand tag={Link} to="/">WebApi</NavbarBrand>
				<ul className="navbar-nav flex-grow">
					<NavItem>
						<NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
					</NavItem>
					<NavItem>
						<NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
					</NavItem>
					<NavItem>
						<NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
					</NavItem>
					<NavItem>
						<LogoutIcon
							onClick={logout}
							titleAccess="Logout"
							style={{ cursor: 'pointer' }}
						>
							Sign Out
						</LogoutIcon>
					</NavItem>
				</ul>
			</Container>
		</Navbar>
	</header>;
};

export default NavMenu;