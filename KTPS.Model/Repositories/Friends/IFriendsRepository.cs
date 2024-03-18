using KTPS.Model.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Friends;

public interface IFriendsRepository
{
    Task<IEnumerable<UserMinimal>> GetFriendListAsync(int userId);
    Task DeleteFriendAsync(int userId, int friendId);
    Task<IEnumerable<UserMinimal>> FindFriendAsync(string input);
    Task InsertAsync(int userId, int friendId);
}