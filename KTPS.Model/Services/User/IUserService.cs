using KTPS.Model.Entities.Registration;
using System.Threading.Tasks;

namespace KTPS.Model.Services.User;

public interface IUserService
{
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
    Task CreateUserAsync(RegistrationBasic registration);
}