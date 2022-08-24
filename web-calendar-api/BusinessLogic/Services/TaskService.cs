using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using AutoMapper;

namespace BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _repository;
    }
}