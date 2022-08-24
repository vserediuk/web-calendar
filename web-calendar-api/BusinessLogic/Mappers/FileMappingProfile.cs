using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Mappers
{
    public class FileMappingProfile : Profile
    {
        public FileMappingProfile()
        {
            CreateMap<FileCreateModel, File>();
            CreateMap<FileViewModel, File>();
            CreateMap<File, FileCreateModel>();
            CreateMap<File, FileViewModel>();
        }
    }
}
