export interface BookData {
  bookId: number;
  title: string;
  bookImg: string;
  author: string;
  publisher: string;
  categoryName: string;
  yearPublished: string;
  quantityAvailable: number;
}

export interface BookDetailData extends BookData {
  description: string;
  quantityAvailable: number;
  status: string;
  createdAt: string;
  createdBy: string;
}

export interface BookBorrowHistory {
  bookId: string;
  readerId: string;
  readerName: string;
  borrowDate: string;
  dueDate: string;
  returnDate: string;
  status: string;
}

export interface LibraryStats {
  totalBooks: number;
  totalReaders: number;
  booksLent: number;
  newBooks: number;
}
