import { BookSummary } from "./book-summary.model";

export interface Category{
    id: string;
    name: string;
    books: BookSummary[];
}