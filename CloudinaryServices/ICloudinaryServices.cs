namespace E_Commerce.CloudinaryServices
{
    public interface ICloudinaryServices
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
