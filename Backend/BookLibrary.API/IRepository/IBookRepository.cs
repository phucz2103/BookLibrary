using BookLibrary.Domain;

namespace BookLibrary.API.IRepository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int bookId);
        Task<Book> GetBookByIdAsync(string bookId);

    }
}
