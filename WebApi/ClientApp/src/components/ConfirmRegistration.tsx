import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import { clientGet } from '../utils/client';

interface ComponentProps {

}

const ConfirmRegistration: React.FC<ComponentProps> = ({

}) => {
	const [text, setText] = useState<string>('');
	const [isConfirmed, setIsConfirmed] = useState<boolean>(false);

	const urlSearchParams = new URLSearchParams(useLocation().search);
	const userId = urlSearchParams.get('userId');
	const token = urlSearchParams.get('token');

	React.useEffect(() => {
		if (userId && token) {
			clientGet(`registration?userId=${userId}&token=${encodeURIComponent(token)}`)
				.then((res) => {
					setText('Your regsitration is confirmed. Click to login: ');
					setIsConfirmed(true);
				})
				.catch((message) => {
					setText(message);
				});
		} else {
			setText('Invalid token or user.');
		}

	}, []);

	return <div>
		{text} {isConfirmed ? <a href='/'> Login </a> : null}
	</div>
};

export default ConfirmRegistration;