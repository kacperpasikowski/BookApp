import { Author } from "./author.model";

export interface BookSummary{
    id: string;
    title: string;
    dateOfPublish: string;
    categoryName: string;    
    bookAvatarUrl: string;
    authors: Author[];
}