using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public UserViewModel GetById(int id);
        UserViewModel Authorize(User user);
        Task<UserViewModel> Register(UserRegistrationModel userRegistrationModel);
    }
}