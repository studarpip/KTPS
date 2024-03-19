using KTPS.Model.Entities.Groups;
using KTPS.Model.Entities.Guests;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.GroupMembers;

public class GroupMembersRepository : IGroupMembersRepository
{
    private readonly IRepository _repository;

    public GroupMembersRepository(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task AddGroupMemberAsync(int userId, int groupId)
    {
        var sql = @"INSERT INTO group_members (GroupID, UserID)
                    VALUES (@GroupId, @UserId)"
        ;

        await _repository.ExecuteAsync<dynamic>(sql, new { GroupId = groupId, UserId = userId });
    }

    public async Task DeleteGroupGuestAsync(int guestId, int groupId)
    {
        var sql = @"
            DELETE FROM guests
            WHERE ID = @GuestId AND GroupID = @GroupId"
        ;

        await _repository.ExecuteAsync<dynamic>(sql, new { GuestId = guestId, GroupId = groupId});
    }

    public async Task DeleteGroupMemberAsync(int userId, int groupId)
    {
        var sql = @"
            DELETE FROM group_members
            WHERE UserID = @UserID AND GroupID = @GroupID;";

        await _repository.ExecuteAsync<dynamic>(sql, new { UserID = userId, GroupID = groupId });
    }

    public async Task<IEnumerable<GroupMember>> GetByGroupIDAsync(int groupId)
    {
        var sql = @"
            SELECT Id, GroupID, UserID from group_members
            WHERE GroupID = @GroupID;";

        return await _repository.QueryListAsync<GroupMember, dynamic>(sql, new { GroupID = groupId });
    }
}