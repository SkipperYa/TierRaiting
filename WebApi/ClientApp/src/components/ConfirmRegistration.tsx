import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import { clientGet } from '../utils/client';
import { useUserContext } from '../utils/userContext';
import { Alert, AlertTitle, Link } from '@mui/material';

interface ComponentProps {

}

const ConfirmRegistration: React.FC<ComponentProps> = ({

}) => {
	const [text, setText] = useState<string>('');
	const [isConfirmed, setIsConfirmed] = useState<boolean>(false);

	const userContext = useUserContext();

	const urlSearchParams = new URLSearchParams(useLocation().search);
	const userId = urlSearchParams.get('userId');
	const token = urlSearchParams.get('token');

	React.useEffect(() => {
		if (userId && token) {
			clientGet(`registration?userId=${userId}&token=${encodeURIComponent(token)}`)
				.then((res) => {
					setText('Your regsitration is confirmed.');
					setIsConfirmed(true);
					const user = userContext.actions.getUser();
					user && userContext.actions.setUser({ ...user, emailConfirmed: true });
				})
				.catch((message) => {
					setText(message);
				});
		} else {
			setText('Invalid token or user.');
		}

	}, []);

	return <div>
		<br />
		{isConfirmed
			? <Alert severity="success">
				<AlertTitle>Success</AlertTitle>
				{text} <Link href={userContext.login ? '/categories' : '/'}> Continue </Link>
			</Alert>
			: <Alert severity="warning">
				<AlertTitle>Warning</AlertTitle>
				{text}
			</Alert>}
	</div>
};

export default ConfirmRegistration;