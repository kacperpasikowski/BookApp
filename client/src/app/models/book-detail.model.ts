import { Author } from "./author.model";
import { Category } from "./category.model";

export interface BookDetail{
    id: string;
    title: string;
    description: string;
    dateOfPublish: string;
    averageGrade: number;
    publisherName: string;
    publisherId: string;
    bookAvatarUrl: string;
    categoryName: string;
    authors: Author[];
    authorIds: string[];
    editing: boolean;
    categories: Category[];
    categoryIds: string[];
}