import type React from "react";
import type { BookData } from "../../types/book";
import { useNavigate } from "react-router-dom";

interface BookTableProps {
  books: BookData[];
  loading: boolean;
  onEdit: (book: BookData) => void;
  onDelete: (bookId: number) => void;
}

const BookTable: React.FC<BookTableProps> = ({
  books,
  loading,
  onEdit,
  onDelete,
}) => {
  const navigate = useNavigate();
  if (loading) {
    return (
      <div className="p-6 text-center">
        <div className="inline-block animate-spin rounded-full h-8 w-8 border-b-2 border-blue-500"></div>
        <p className="mt-2 text-gray-600">Đang tải dữ liệu...</p>
      </div>
    );
  }
  return (
    <div className="overflow-x-auto">
      <table className="w-full">
        <thead className="bg-gray-50">
          <tr>
            <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Mã Sách
            </th>
            <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Tên Sách
            </th>
            <th className="px-4 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Ảnh Sách
            </th>
            <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Tác Giả
            </th>
            <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Nhà XB
            </th>
            <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Năm XB
            </th>
            <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Thể loại
            </th>
            <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Số lượng sẵn
            </th>
            <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
              Hành động
            </th>
          </tr>
        </thead>
        <tbody className="bg-white divide-y divide-gray-200">
          {books.map((book) => (
            <tr key={book.bookId} className="hover:bg-gray-50">
              <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                {book.bookId}
              </td>
              <td className="px-6 py-4 text-sm text-gray-900">
                <p className="line-clamp-2">{book.title}</p>
              </td>
              <td className="text-center text-sm text-gray-900 align-middle">
                <img
                  src={book.bookImg}
                  alt="Book Image"
                  className="w-[100px] h-[150px] object-cover"
                />
              </td>
              <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {book.author}
              </td>
              <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {book.publisher}
              </td>
              <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {book.yearPublished}
              </td>
              <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {book.categoryName}
              </td>
              <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                {book.quantityAvailable}
              </td>
              <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                <button
                  onClick={() => navigate(`/book/${book.bookId}`)}
                  className="text-blue-600 hover:text-blue-900 mr-3"
                >
                  Chi tiết
                </button>
                <button
                  onClick={() => onDelete(book.bookId)}
                  className="text-red-600 hover:text-red-900"
                >
                  Xóa
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default BookTable;
