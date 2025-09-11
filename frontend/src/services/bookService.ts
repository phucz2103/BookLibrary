import axios from "axios";
import type { BookData, BookDetailData } from "../types/book";

const API_BASE_URL = "https://localhost:5001/book";

export const bookService = {
  async getBooks(): Promise<BookData[]> {
    try {
            console.log("Fetching book list");
      const response = await axios.get<BookData[]>(`${API_BASE_URL}/list-book`);
      return response.data;
    } catch (error) {
      console.error("Lỗi khi lấy danh sách sách:", error);
      throw error;
    }
  },

  async getDetailBook(id: number): Promise<BookDetailData | string> {
    try {
      console.log("Fetching book details for ID:", id);
      const response = await axios.get<BookDetailData>(
        `${API_BASE_URL}/book-detail/${id}`
      );
      console.log("Received book details:", response.data);
      return response.data;
    } catch (error) {
      console.log("lỗi rồi");
      console.error("Lỗi không lấy được chi tiết sách:", error);
      throw error;
    }
  },
};
