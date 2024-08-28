import { AuthorDetail } from "./author-detail-model";
import { BookDetail } from "./book-detail.model";

export interface User{
    id: string;
    userName: string;
    userAvatarUrl: string;
    token: string;
    readBooks: BookDetail[];
    favoriteAuthors: AuthorDetail[];
    friends: User[];
}