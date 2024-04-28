import { Item } from "./Item";
import { CategoryType } from "./enums";

export interface Category {
	id: string;
	title: string;
	description: string;
	categoryType: CategoryType;
	items: Array<Item>;
	itemsCount: number;
	src: string;
}

export const CategotyTypeNames = {
	[CategoryType.None]: 'None',
	[CategoryType.Games]: 'Games',
	[CategoryType.Films]: 'Films',
	[CategoryType.Books]: 'Books',
}