import { Category } from "./Category";
import { Tier } from "./enums";

export interface Item {
	id: string;
	title: string;
	description: string;
	src: string;
	tier: Tier;
	category: Category | undefined;
	categoryId: string;
}

export const tierColors = {
	[Tier.S]: '#ffa333',
	[Tier.A]: '#ad33ff',
	[Tier.B]: '#4c57f5',
	[Tier.C]: '#26eb54',
	[Tier.D]: '#a1a1a1',
	[Tier.None]: '#ffff',
};

export const tierNames = {
	[Tier.S]: 'S',
	[Tier.A]: 'A',
	[Tier.B]: 'B',
	[Tier.C]: 'C',
	[Tier.D]: 'D',
	[Tier.None]: 'None',
};