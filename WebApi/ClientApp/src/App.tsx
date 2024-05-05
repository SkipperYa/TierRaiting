import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Registration from './components/Registration';
import Dashboard from './components/Dashboard';
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
		<Route exact path='/' component={Login} />
		<Route path='/registration' component={Registration} />
		<Route path='/categories' component={Categories} />
		<Route path='/category' component={CategoryEditor} />
		<Route path='/confirmRegistration' component={ConfirmRegistration} />
		<Route path='/dashboard' component={Dashboard} />
		<Route path='/profile' component={Profile} />
	</Layout>;
};

export default App;
