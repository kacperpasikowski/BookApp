import { BookSummary } from "./book-summary.model";

export interface AuthorDetail{
    id: string;
    name: string;
    authorAvatarUrl: string;
    mainCategory: string;
    dateOfBirth: string;
    books: BookSummary[];
}