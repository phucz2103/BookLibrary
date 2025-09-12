using BookLibrary.API.IService;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;

namespace BookLibrary.API.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILoggerFactory _logger;

        public CloudinaryService(IConfiguration config, ILoggerFactory logger)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
            _logger = logger;
        }
        public async Task<string> UploadImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty or null");
            }

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp", "image/tiff", "image/heic" };

            if (file != null)
            {
                if (!allowedTypes.Contains(file.ContentType))
                    throw new ArgumentException("Vui lòng chọn ảnh có định dạng jpeg/png/bmp/gif/webp/tiff/heic");
            }
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false
            };

            _logger.CreateLogger($"Starting upload to folder: {folder}, File: {file.FileName}");
            Console.WriteLine("vao day r");

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                _logger.CreateLogger($"Cloudinary upload failed: {uploadResult.Error.Message}");
                throw new Exception($"Image upload failed: {uploadResult.Error.Message}");
                Console.WriteLine("loi r");

            }

            // Log thông tin upload thành công
            _logger.CreateLogger($"Upload successful! PublicId: {uploadResult.PublicId}, " +
                $"URL: {uploadResult.SecureUrl}, Size: {uploadResult.Bytes} bytes");
            Console.WriteLine("thanh cong roi");

            return uploadResult.SecureUrl.ToString();
        }
    }
}
