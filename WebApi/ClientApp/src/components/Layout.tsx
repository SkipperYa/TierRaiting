import * as React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';

export const Layout: React.FC = ({
	children
}) => {
	return (
		<React.Fragment>
			<NavMenu />
			<Container>
				{children}
			</Container>
		</React.Fragment>
	);
}

export default Layout;