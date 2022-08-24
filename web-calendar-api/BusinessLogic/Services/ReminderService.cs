using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using AutoMapper;

namespace BusinessLogic.Services
{
    public class ReminderService : IReminderService
    {
        private IMapper _mapper;
        private IReminderRepository _repository;
    }
}