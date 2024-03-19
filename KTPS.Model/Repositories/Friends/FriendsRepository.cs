using KTPS.Model.Entities.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Friends;

public class FriendsRepository : IFriendsRepository
{
    private readonly IRepository _repository;

    public FriendsRepository(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserMinimal>> GetFriendListAsync(int userId)
    {
        var sql = @"
            SELECT u.ID, u.Username FROM users AS u
            LEFT JOIN friends ON friends.UserID = u.ID
            WHERE friends.FriendId = @ID;";

        return await _repository.QueryListAsync<UserMinimal, dynamic>(sql, new { ID = userId });
    }

    public async Task DeleteFriendAsync(int userId, int friendId)
    {
        var sql = @"
            DELETE FROM friends
            WHERE UserID = @UserID AND FriendId = @FriendID;";

        await _repository.ExecuteAsync<dynamic>(sql, new { UserID = userId, FriendID = friendId });
    }

    public async Task<IEnumerable<UserMinimal>> FindFriendAsync(string input)
    {
        var sql = @$"
            SELECT ID, Username FROM users
            WHERE Username LIKE '%{input}%'";

        return await _repository.QueryListAsync<UserMinimal, dynamic>(sql, new { });
    }

    public async Task InsertAsync(int userId, int friendId)
    {
        var sql = @"
            INSERT INTO friends (`UserId`, `FriendID`)
            VALUES (@UserID, @FriendID);";

        await _repository.ExecuteAsync(sql, new { UserID = userId, FriendID = friendId });
    }
}