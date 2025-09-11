import type React from "react";
import type { BookDetailData } from "../../types/book";
import { BookOpen, Calendar, Hash, User } from "lucide-react";

interface BookInforProps{
    book: BookDetailData;
    onEdit:() => void;
}

const BookDetail : React.FC<BookInforProps> = ({book, onEdit}) =>{
    const getStatusColor = (status: BookDetailData['status']) => {
    switch (status) {
      case 'có sẵn':
        return 'bg-green-100 text-green-800';
      case 'còn ít':
        return 'bg-yellow-100 text-yellow-800';
      case 'hết':
        return 'bg-red-100 text-red-800';
      default:
        return 'bg-gray-100 text-gray-800';
    }
    };

    const getStatusText = (status: BookDetailData['status']) => {
        switch (status) {
      case 'có sẵn':
        return 'Có sẵn';
      case 'còn ít':
        return 'Còn ít';
      case 'hết':
        return 'Hết';
      default:
        return 'Không xác định';
    }
    };

    return (
    <div className="bg-white rounded-lg shadow-md overflow-hidden">
      <div className="px-6 py-4 bg-gray-50 border-b border-gray-200 flex justify-between items-center">
        <h2 className="text-xl font-semibold text-gray-800">Thông Tin Sách</h2>
        <button
          onClick={onEdit}
          className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors"
        >
          Chỉnh sửa
        </button>
      </div>

      <div className="p-6">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          {/* Thông tin cơ bản */}
          <div className="space-y-4">
            <div className="flex items-center space-x-2">
              <Hash className="w-5 h-5 text-gray-500" />
              <span className="text-sm font-medium text-gray-600">Mã sách:</span>
              <span className="text-sm text-gray-900 font-mono">{book.bookId}</span>
            </div>

              <div className="flex items-center space-x-2">
              <Hash className="w-5 h-5 text-gray-500" />
              <span className="text-sm font-medium text-gray-600">Ảnh sách:</span>
              <span className="text-sm text-gray-900 font-mono"><img
                  src={book.bookImg}
                  alt="Book Image"
                  className="w-[80px] h-auto mx-auto"/>
                  </span>
            </div>

            <div>
              <h3 className="text-lg font-semibold text-gray-900 mb-2">{book.title}</h3>
            </div>

            <div className="flex items-center space-x-2">
              <User className="w-5 h-5 text-gray-500" />
              <span className="text-sm font-medium text-gray-600">Tác giả:</span>
              <span className="text-sm text-gray-900">{book.author}</span>
            </div>

            <div className="flex items-center space-x-2">
              <BookOpen className="w-5 h-5 text-gray-500" />
              <span className="text-sm font-medium text-gray-600">Nhà xuất bản:</span>
              <span className="text-sm text-gray-900">{book.pushliser}</span>
            </div>

            <div className="flex items-center space-x-2">
              <Calendar className="w-5 h-5 text-gray-500" />
              <span className="text-sm font-medium text-gray-600">Năm xuất bản:</span>
              <span className="text-sm text-gray-900">{book.yearPublished}</span>
            </div>

          {/* Thông tin chi tiết */}
          <div className="space-y-4">
            <div className="flex items-center space-x-2">
              <span className="text-sm font-medium text-gray-600">Trạng thái:</span>
              <span className={`px-3 py-1 rounded-full text-xs font-medium ${getStatusColor(book.status)}`}>
                {getStatusText(book.status)}
              </span>
            </div>

            <div className="flex items-center space-x-2">
              <span className="text-sm font-medium text-gray-600">Thể loại:</span>
              <span className="text-sm text-gray-900">{book.categoryName}</span>
            </div>

        {book.description && (
          <div className="mt-6">
            <h4 className="text-sm font-medium text-gray-600 mb-2">Mô tả:</h4>
            <p className="text-sm text-gray-900 leading-relaxed">{book.description}</p>
          </div>
        )}

        <div className="mt-6 pt-4 border-t border-gray-200 grid grid-cols-1 md:grid-cols-2 gap-4 text-xs text-gray-500">
          <div>
            <span className="font-medium">Ngày thêm:</span> {new Date(book.createdAt).toLocaleDateString('vi-VN')}
          </div>
          <div>
            <span className="font-medium">Nguời thêm: {book.createdBy}</span>
          </div>
        </div>
      </div>
    </div>
    </div>
    </div>
    </div>
  );
};

export default BookDetail;