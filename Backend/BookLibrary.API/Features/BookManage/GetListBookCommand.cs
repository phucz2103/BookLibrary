using BookLibrary.API.IRepository;
using MediatR;
using System.Security.Claims;

namespace BookLibrary.API.Features.ListBook
{
    public class GetListBookCommand : IRequest<List<BookListResponse>>
    {
        public GetListBookCommand() { }
    }

    public class GetListBookCommandHandler : IRequestHandler<GetListBookCommand, List<BookListResponse>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IHttpContextAccessor _http;
        public GetListBookCommandHandler(IBookRepository bookRepository, IHttpContextAccessor http)
        {
            _bookRepository = bookRepository;
            _http = http;
        }
        public async Task<List<BookListResponse>> Handle(GetListBookCommand request, CancellationToken cancellationToken)
        {
            var user = _http.HttpContext.User;
            var currentRole = user?.FindFirst(ClaimTypes.Role)?.Value;

            if (currentRole == "User")
            {
                throw new UnauthorizedAccessException("Không có quyền truy cập");
            }

            var books = await _bookRepository.GetAllBooksAsync();

            var result = new List<BookListResponse>();
            return result = books.Select(b => new BookListResponse
            {
                BookId = b.BookId,
                Title = b.Title,
                BookImg = b.BookImg,
                Author = b.Author.Name,
                Publisher = b.Publisher.Name,
                CategoryName = b.Category != null ? b.Category.Name : "khác",
                YearPublished = b.PublishedDate.ToString("dd/MM/yyyy"),
                QuantityAvailable = b.AvailableQuantity,
            }).ToList();
        }
    }
}
