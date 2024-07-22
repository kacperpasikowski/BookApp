import { Author } from "./author.model";
import { Category } from "./category.model";

export interface BookDetail{
    id: string;
    title: string;
    description: string;
    dateOfPublish: string;
    publisherName: string;
    publisherId: string;
    bookAvatarUrl: string;
    authors: Author[];
    authorIds: string[];
    editing: boolean;
    categories: Category[];
    categoryIds: string[];
}