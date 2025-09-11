using BookLibrary.API.IRepository;
using BookLibrary.Data;
using BookLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _context;
        public BookRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task AddBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await _context.Books.Include(b => b.Category).Include(b => b.Author).Include(b => b.Publisher).ToListAsync();
            return books;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            var book =await  _context.Books.Include(b => b.Category).Include(b => b.Author).Include(b => b.Publisher).FirstOrDefaultAsync(b => b.BookId == bookId);
            return book;
        }
        public Task UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
