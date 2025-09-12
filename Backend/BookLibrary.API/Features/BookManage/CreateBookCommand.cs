using BookLibrary.API.Data;
using BookLibrary.API.IRepository;
using BookLibrary.API.IService;
using BookLibrary.API.Repositories;
using BookLibrary.Domain;
using MediatR;

namespace BookLibrary.API.Features.BookManagement
{
    public class CreateBookCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public int? PublisherId { get; set; }
        public DateTime PublishedDate { get; set; }
        public IFormFile BookImg { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
    }

    public class CreateBookHandle : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IDBContext _dBContext;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IHttpContextAccessor _http;
        private readonly IUserRepository _userRepo;
        public CreateBookHandle(IDBContext dBContext, ICloudinaryService cloudinaryService, IUserRepository userRepo, IHttpContextAccessor http)
        {
            _dBContext = dBContext;
            _cloudinaryService = cloudinaryService;
            _userRepo = userRepo;
            _http = http;
        }
        public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellation)
        {

            var bookRepo = new BaseRepository<Book>(_dBContext);
            var authorRepo = new BaseRepository<Author>(_dBContext);
            var publisherRepo = new BaseRepository<Publishers>(_dBContext);
            var categoryRepo = new BaseRepository<Category>(_dBContext);
            var imageUrl = "bookimg";

            var user = _http.HttpContext.User;
            var userId = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var currentRole = user?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            //if (currentRole == "User")
            //{
            //    throw new UnauthorizedAccessException("Không có quyền truy cập");
            //}

            if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Description)){
                throw new Exception("Tiêu đề và mô tả không được để trống");
            }

            if (request.Quantity <= 0)
            {
                throw new Exception("Số lượng phải lớn hơn 0");
            }

            if (request.PublishedDate > DateTime.Now)
            {
                throw new Exception("Ngày xuất bản không hợp lệ");
            }

            var author = await authorRepo.GetByIdAsync(request.AuthorId ?? 0);
            if (author == null)
            {
                throw new Exception("Tác giả không tồn tại");
            }

            var publisher = await publisherRepo.GetByIdAsync(request.PublisherId ?? 0);
            if (publisher == null)
            {
                throw new Exception("Nhà xuất bản không tồn tại");
            }

            var category = await categoryRepo.GetByIdAsync(request.CategoryId ?? 0);
            if (category == null && request.CategoryId != null)
            {
                throw new Exception("Thể loại không tồn tại");
            }

            imageUrl = await _cloudinaryService.UploadImageAsync(request.BookImg, "BookImg");


            return await bookRepo.AddAsync(new Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId,
                PublisherId = request.PublisherId,
                PublishedDate = request.PublishedDate,
                Description = request.Description,
                Quantity = request.Quantity,
                AvailableQuantity = request.Quantity,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.Parse(userId),
                Status = true,
                BookImg =imageUrl
            });
        }

    }
}
