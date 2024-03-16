using KTPS.Model.Entities.User;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.User;

public interface IUserRepository
{
    Task<UserBasic> GetByUsernameAsync(string username);
    Task<UserBasic> GetByEmailAsync(string email);
    Task<int> InsertAsync(UserBasic user);
    Task<UserBasic> GetByIdAsync(int id);
    Task UpdateUserAsync(UserBasic updatedUser);
}