using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Friends;

public interface IFriendsService
{
    Task<ServerResult<IEnumerable<UserMinimal>>> GetFriendListAsync(int userId);
    Task<ServerResult> DeleteFriendAsync(DeleteFriendRequest request);
    Task<ServerResult<IEnumerable<UserMinimal>>> FindFriendAsync(FindFriendRequest request);
    Task AddFriendAsync(int userId, int friendId);
}