import { BookSummary } from "./book-summary.model";

export interface Publisher{
    id: string;
    name:string;
    books: BookSummary[];
}