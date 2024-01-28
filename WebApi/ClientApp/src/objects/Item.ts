import { Category } from "./Category";

export interface Item {
	id: string;
	title: string;
	description: string;
	category: Category;
	categoryId: string;
}