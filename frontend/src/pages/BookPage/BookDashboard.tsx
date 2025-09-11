import { useEffect, useState } from "react";
import type { BookData, LibraryStats } from "../../types/book";
import { bookService } from "../../services/bookService";
import StatCard from "../../components/Common/StatCard";
import { Archive, Book, FileText, User } from "lucide-react";
import BookTable from "../../components/books/BookTable";
import { useLayoutContext } from "../../layouts/LayoutWrapper";

const BookDashboard: React.FC = () => {
  const [stats, setStats] = useState<LibraryStats>({
    totalBooks: 0,
    totalReaders: 0,
    booksLent: 0,
    newBooks: 0,
  });

  const { searchTerm } = useLayoutContext();

  const [books, setBooks] = useState<BookData[]>([]);
  const [loading, setLoading] = useState(false);
  useEffect(() => {
    (async () => {
      setLoading(true);
      try {
        const response = await bookService.getBooks();
        setBooks(response);
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  const filteredBooks = books.filter(
    (book) =>
      book.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
      book.author.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleEdit = (book: BookData) => {
    console.log("Edit book:", book);
    // Implement edit logic
  };

  const handleDelete = (bookId: number) => {
    console.log("Delete book:", bookId);
    // Implement delete logic
  };
  return (
    <div className="space-y-6">
      {/* Statistics Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <StatCard
          title="Tổng số sách"
          value={stats.totalBooks}
          color="border-l-blue-500"
          icon={<Book className="w-6 h-6 text-blue-500" />}
        />
        <StatCard
          title="Số độc giả"
          value={stats.totalReaders}
          color="border-l-green-500"
          icon={<User className="w-6 h-6 text-green-500" />}
        />
        <StatCard
          title="Sách đã cho mượn"
          value={stats.booksLent}
          color="border-l-yellow-500"
          icon={<FileText className="w-6 h-6 text-yellow-500" />}
        />
        <StatCard
          title="Sách mới"
          value={stats.newBooks}
          color="border-l-purple-500"
          icon={<Archive className="w-6 h-6 text-purple-500" />}
        />
      </div>

      {/* Recent Books Table */}
      <div className="bg-white rounded-lg shadow-md">
        <div className="px-6 py-4 border-b border-gray-200">
          <h3 className="text-lg font-semibold text-gray-800">
            Danh Sách Sách Mới Nhất
          </h3>
        </div>

        <BookTable
          books={filteredBooks}
          loading={loading}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      </div>
    </div>
  );
};

export default BookDashboard;
