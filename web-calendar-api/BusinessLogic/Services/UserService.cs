using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<User> _userManager;
        private readonly ICalendarService _calendarService;

        public UserService(IMapper mapper, IUserRepository repository, IJwtGenerator jwtGenerator,
            UserManager<User> userManager, ICalendarService calendarService)
        {
            _mapper = mapper;
            _repository = repository;
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
            _calendarService = calendarService;
        }

        public UserViewModel GetById(int id) => _mapper.Map<UserViewModel>(_repository.GetById(id));

        public UserViewModel Authorize(User user)
        {
            var result = _mapper.Map<UserViewModel>(user);
            result.Token = _jwtGenerator.CreateToken(user);
            return result;
        }

        public async Task<UserViewModel> Register(UserRegistrationModel userRegistrationModel)
        {
            var user = _mapper.Map<User>(userRegistrationModel);
            var result = await _userManager.CreateAsync(user, userRegistrationModel.Password);
            if (!result.Succeeded) return null;
            var calendar = new CalendarCreateModel()
                {Description = "", Name = "My first calendar", UserId = user.Id};
            _calendarService.Create(calendar);
            return Authorize(user);
        }
    }
}