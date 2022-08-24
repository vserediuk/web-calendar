using DataAccess.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}