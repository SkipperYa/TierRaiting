export const baseUrl = 'api/';

export const setUser = (user: any) => {
	localStorage.setItem('user', JSON.stringify(user));
};

export const getUser = () => {
	const item = localStorage.getItem('user');
	return item ? JSON.parse(item) : null;
};

export const removeUser = () => {
	localStorage.removeItem('user');
};

const handleResponse = (response: Response): Promise<any> => {
	if (response.status === 200) {
		return response.json().then((data: any) => {
			return data;
		});
	} else if (response.status === 401) {
		window.location.href = '/';
		return Promise.reject();
	} else {
		return response.json().then((error: any) => {
			throw error;
		});
	};
}

export const clientGet = (path: string) => {
	return fetch(`${baseUrl}${path}`, {
		credentials: 'same-origin',
		method: 'GET',
		headers: {
			'Content-type': 'application/json; charset=utf-8',
		}
	}).then((response) => {
		return handleResponse(response);
	}).catch((error) => {
		console.log(error);
	});
};

export const clientDelete = (path: string, data: any) => {
	return fetch(`${baseUrl}${path}`, {
		credentials: 'same-origin',
		method: 'DELETE',
		headers: {
			'Content-type': 'application/json; charset=utf-8',
		},
		body: JSON.stringify(data)
	}).then((response) => {
		return handleResponse(response);
	}).catch((error) => {
		console.log(error);
	});
};

export const clientUpdate = (path: string, data: any) => {
	return fetch(`${baseUrl}${path}`, {
		credentials: 'same-origin',
		method: 'PUT',
		headers: {
			'Content-type': 'application/json; charset=utf-8',
		},
		body: JSON.stringify(data)
	}).then((response) => {
		return handleResponse(response);
	}).catch((error) => {
		console.log(error);
	});
};

export const clientPost = (path: string, data: any) => {
	return fetch(`${baseUrl}${path}`, {
		credentials: 'same-origin',
		method: 'POST',
		headers: {
			'Content-type': 'application/json; charset=utf-8',
		},
		body: JSON.stringify(data)
	}).then((response) => {
		return handleResponse(response);
	}).catch((error) => {
		console.log(error);
		throw error;
	});
};

export const clientUpload = (path: string, file: any) => {
	const data = new FormData();
	data.append('file', file);

	return fetch(`${baseUrl}${path}`, {
		credentials: 'same-origin',
		method: 'POST',
		body: data,
	}).then((response) => {
		return handleResponse(response);
	}).catch((error) => {
		console.log(error);
		throw error;
	});
};