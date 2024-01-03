export const baseUrl = 'api/';

const handleResponse = (response: Response): Promise<any> => {
	return response.json().then((data: any) => {
		if (response.status === 200) {
			return data;
		} else {
			throw data;
		}
	});
}

export const clientGet = (path: string) => {
	return fetch(`${baseUrl}${path}`).then((response) => {
		return handleResponse(response);
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
	}).then((response) => handleResponse(response));
};