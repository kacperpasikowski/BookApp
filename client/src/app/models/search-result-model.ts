import { Author } from "./author.model";
import { Category } from "./category.model";

export interface SearchResult{
    id: string;
    titleOrAuthorName: string;
    type: string;
    categories: Category[];
    authors: Author[];
    mainCategory: string;
    avatarUrl: string;
}