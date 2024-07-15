import { BookSummary } from "./book-summary.model";

export interface AuthorDetail{
    id: string;
    name: string;
    authorAvatarUrl: string;
    dateOfBirth: string;
    books: BookSummary[];
}