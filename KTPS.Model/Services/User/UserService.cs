using KTPS.Model.Entities.Registration;
using KTPS.Model.Entities.User;
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

    public async Task<int> CreateUserAsync(RegistrationBasic registration)
    {
        return await _userRepository.InsertAsync(new() { Email = registration.Email, Username = registration.Username, Password = registration.Password });
    }

    public async Task<UserBasic> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetByUsernameAsync(username);
    }

    public async Task<UserBasic> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<UserBasic> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task UpdateUserAsync(UserBasic updatedUser)
    {
        await _userRepository.UpdateUserAsync(updatedUser);
    }
}