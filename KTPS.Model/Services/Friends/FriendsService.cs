using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Entities.User;
using KTPS.Model.Repositories.Friends;
using KTPS.Model.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Friends;

public class FriendsService : IFriendsService
{
    private readonly IFriendsRepository _friendsRepository;
    private readonly IUserRepository _userRepository;

    public FriendsService(
        IFriendsRepository friendsRepository,
        IUserRepository userRepository
        )
    {
        _friendsRepository = friendsRepository;
        _userRepository = userRepository;
    }

    public async Task<ServerResult<IEnumerable<UserMinimal>>> GetFriendListAsync(int userId)
    {
        try
        {
            var friends = await _friendsRepository.GetFriendListAsync(userId);
            return new() { Success = true, Data = friends };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult> DeleteFriendAsync(DeleteFriendRequest request)
    {
        try
        {
            await _friendsRepository.DeleteFriendAsync(request.UserID, request.FriendID);
            await _friendsRepository.DeleteFriendAsync(request.FriendID, request.UserID);
            return new() { Success = true };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult<IEnumerable<UserMinimal>>> FindFriendAsync(FindFriendRequest request)
    {
        try
        {
            var availableFriends = await _friendsRepository.FindFriendAsync(request.Input);
            var filteredFriends = availableFriends.Where(x => x.ID != request.UserID);
            return new() { Success = true, Data = filteredFriends };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }
}