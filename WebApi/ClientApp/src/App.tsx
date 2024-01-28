import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Counter from './components/Counter';
import Registration from './components/Registration';
import Login from './components/Login';
import FetchData from './components/FetchData';
import Categories from './components/Categories';

import './custom.css';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

export default () => (
	<Layout>
		<Route exact path='/' component={Login} />
		<Route path='/counter' component={Counter} />
		<Route path='/registration' component={Registration} />
		<Route path='/categories' component={Categories} />
		<Route path='/fetch-data/:startDateIndex?' component={FetchData} />
	</Layout>
);
