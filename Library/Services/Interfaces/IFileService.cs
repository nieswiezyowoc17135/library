using Library.Services.Models;

namespace Library.Services.Interfaces
{
    public interface IFileService
    {
        Task<bool> AddingToDatabase(IFormFile filePath);
        Task<byte[]> CreatingFile();
    }
}
