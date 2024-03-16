using KTPS.Model.Entities.Registration;
using KTPS.Model.Helpers;
using KTPS.Model.Repositories.User;
using System.Threading.Tasks;

namespace KTPS.Model.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository
        )
    {
        _userRepository = userRepository;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return user is not null;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user is not null;
    }

    public async Task CreateUserAsync(RegistrationBasic registration)
    {
        var hashedPassword = registration.Password.Hash();
        await _userRepository.InserAsync(new() { Email = registration.Email, Username = registration.Username, Password = hashedPassword });
    }
}