import 'bootstrap/dist/css/bootstrap.css';

import { createBrowserHistory } from 'history';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Router } from 'react-router';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { UserContextProvider } from './utils/userContext';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;
const history = createBrowserHistory({ basename: baseUrl });

ReactDOM.render(
	<Router history={history}>
		<UserContextProvider>
			<App />
		</UserContextProvider>
	</Router>,
	document.getElementById('root'));

registerServiceWorker();
