import { BookSummary } from "./book-summary.model";

export interface Author{
    id: string;
    name: string;
    books: BookSummary[];
}