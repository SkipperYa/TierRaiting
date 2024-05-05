import * as React from 'react';
import { Route, Switch } from 'react-router';
import Layout from './components/Layout';
import Registration from './components/Registration';
import Login from './components/Login';
import Categories from './components/Categories';
import CategoryEditor from './components/CategoryEditor';
import ConfirmRegistration from './components/ConfirmRegistration';
import Profile from './components/Profile';

import './custom.css';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

const App: React.FC = () => {
	return <Layout>
		<Switch>
			<Route exact path='/' component={Login} />
			<Route exact path='/registration' component={Registration} />
			<Route exact path='/categories' component={Categories} />
			<Route exact path='/category' component={CategoryEditor} />
			<Route exact path='/confirmRegistration' component={ConfirmRegistration} />
			<Route exact path='/profile' component={Profile} />
			<Route path='*' component={() => <h1 style={{ marginBlock: '10%' }} className="text-center">404. Page not found.</h1>} />
		</Switch>
	</Layout>;
};

export default App;
