using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.Interfaces
{
    public interface IFileService
    {
        public FileViewModel Create(IFormFile file);
        public FileViewModel GetById(int id);
    }
}
