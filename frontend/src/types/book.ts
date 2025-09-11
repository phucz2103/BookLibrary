export interface BookData {
  bookId: number;
  title: string;
  bookImg: string;
  author: string;
  pushliser: string;
  categoryName: string;
  yearPublished: string;
  quantityAvailable: number;
}

export interface BookDetailData {
  bookId: number;
  bookImg: string;
  title: string;
  author: string;
  publisher: string;
  timePublish: string;
  description: string;
  categoryName: string;
  quantity: number;
  quantityAvailable: number;
  status: string;
  createdAt: string;
  createdBy: string;
}

export interface LibraryStats {
  totalBooks: number;
  totalReaders: number;
  booksLent: number;
  newBooks: number;
}
