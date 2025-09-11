import { useEffect, useState } from "react";
import { bookService } from "../../services/bookService";
import BookDetail from "../../components/books/bookInfor";
import { useParams } from "react-router-dom";
import type { BookDetailData } from "../../types/book";

const BookDetailPage : React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [error, setError] = useState<string>("");
    const [isLoading, setIsLoading] = useState(true);
    const [book, setBook] = useState<BookDetailData | null>(null);

    useEffect(() => {
        const fetchBookDetail = async () => {
            try{
                setIsLoading(true);
                setError("");
                if (id) {
                    const response = await bookService.getDetailBook(Number(id));
                    if (typeof response === "object" && response !== null) {
                        setBook(response as BookDetailData);
                    } else {
                        setError(response as string);
                    }
                }
            } catch (error) {
                console.error("Lỗi khi lấy chi tiết sách:", error);
            }finally{
                setIsLoading(false);
            }
        };
        fetchBookDetail();
    }, [id]);

    if (isLoading) {
        return <div className="p-4">Đang tải thông tin sách...</div>;
    }

    if (error) {
        return <div className="p-4 text-red-500">{error}</div>;
    }

    if (!book) {
        return <div className="p-4">Không tìm thấy thông tin sách</div>;
    }
    return (
        <div className="container mx-auto p-4">
            <BookDetail
                book={book} 
                onEdit={() => {
                    // Xử lý logic chỉnh sửa ở đây
                    console.log("Edit book:", book.bookId);
                }} 
            />
        </div>
    );
}

export default BookDetailPage;