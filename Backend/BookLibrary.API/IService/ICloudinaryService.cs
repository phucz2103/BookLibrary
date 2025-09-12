namespace BookLibrary.API.IService
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file, string folder );
    }
}
