import { Item } from "./Item";
import { UserImage } from "./UserImage";

export interface Category {
	id: string;
	title: string;
	description: string;
	items: Array<Item>;
	itemsCount: number;
	src: string;
	imageId: string | undefined;
	image: UserImage | undefined;
}