using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private readonly IMapper _mapper;
        private readonly IFileRepository _repository;
        private readonly IHostEnvironment _hostEnvironment;

        public FileService(IHostEnvironment hostEnvironment, IFileRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }
        public FileViewModel Create(IFormFile file)
        {
            var uniqueFileName = GetUniqueFileName(file.FileName);
            var dir = Path.Combine(_hostEnvironment.ContentRootPath, "Uploads");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filePath = Path.Combine(dir, uniqueFileName);

            FileCreateModel fileCreateModel = new FileCreateModel
            {
                Name = file.FileName,
                Path = filePath,
                DateUploaded = DateTime.Now,
                Size = (int)(file.Length / (1024 * 1024))
            };

            if (fileCreateModel.Size > 100)
                throw new BadRequestException("Max size of a file is 100MB");

            var fileEntity = _mapper.Map<DataAccess.Entities.File>(fileCreateModel);
            var repositoryEntityAddedResult = _repository.Add(fileEntity);
            var result = _mapper.Map<FileViewModel>(repositoryEntityAddedResult);

            CreateFileOnDisk(file, filePath);
            return result;
        }

        public FileViewModel GetById(int id)
        {
            return _mapper.Map<FileViewModel>(_repository.GetById(id));
        }

        private async void CreateFileOnDisk(IFormFile file, string filePath)
        {
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
        private string GetUniqueFileName(string fileName)
        {
            return Guid.NewGuid().ToString().Substring(0, 10) + Path.GetExtension(fileName);
        }
    }
}
