using BookLibrary.API.IRepository;
using MediatR;
using System.Security.Claims;

namespace BookLibrary.API.Features.ListBook
{
    public class GetBookDetailCommand : IRequest<BookDetailResponse>
    {
        public int BookId { get; set; }
        public GetBookDetailCommand(int bookId)
        {
            BookId = bookId;
        }
    }

    public class GetBookDetailCommandHandler : IRequestHandler<GetBookDetailCommand, BookDetailResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IHttpContextAccessor _http;
        private readonly IUserRepository _userRepository;
        public GetBookDetailCommandHandler(IBookRepository bookRepository, IHttpContextAccessor http, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _http = http;
            _userRepository = userRepository;
        }
        public async Task<BookDetailResponse> Handle(GetBookDetailCommand request, CancellationToken cancellationToken)
        {
            var user = _http.HttpContext.User;
            var currentRole = user?.FindFirst(ClaimTypes.Role)?.Value;
            if (currentRole == "User")
            {
                throw new UnauthorizedAccessException("Không có quyền truy cập");
            }
            var book = await _bookRepository.GetBookByIdAsync(request.BookId);
            if (book == null)
            {
                throw new Exception("Không tìm thấy sách");
            }

            var createdByUser = await _userRepository.GetUserByIdAsync(book.CreatedBy);

            return new BookDetailResponse
            {
                BookId = book.BookId,
                BookImg = book.BookImg,
                Title = book.Title,
                YearPublished = book.PublishedDate.Year,
                Publisher = book.Publisher?.Name,
                Author = book.Author.Name,
                Description = book.Description,
                Quantity = book.Quantity,
                QuantityAvailable = book.AvailableQuantity,
                CategoryName = book.Category != null ? book.Category.Name : "khác",
                Status = book.Status,
                CreatedAt = book.CreatedAt,
                CreatedBy = createdByUser.FullName
            };
        }
    }
}
