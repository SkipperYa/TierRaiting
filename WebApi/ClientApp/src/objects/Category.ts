import { Item } from "./Item";

export interface Category {
	id: string;
	title: string;
	description: string;
	items: Array<Item>;
	itemsCount: number;
}