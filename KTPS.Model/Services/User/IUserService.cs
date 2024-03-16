using KTPS.Model.Entities.Registration;
using KTPS.Model.Entities.User;
using System.Threading.Tasks;

namespace KTPS.Model.Services.User;

public interface IUserService
{
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
    Task<int> CreateUserAsync(RegistrationBasic registration);
    Task<UserBasic> GetUserByUsernameAsync(string username);
    Task<UserBasic> GetUserByEmailAsync(string email);
    Task<UserBasic> GetUserByIdAsync(int id);
    Task UpdateUserAsync(UserBasic updatedUser);
}