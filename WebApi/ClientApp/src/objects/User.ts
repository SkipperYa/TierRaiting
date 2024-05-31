export interface Profile {
	id: string;
	userName: string;
	emailConfirmed: boolean;
	src: string;
	email: string;
}

export interface User extends Profile {
	lockoutEnabled: boolean;
}