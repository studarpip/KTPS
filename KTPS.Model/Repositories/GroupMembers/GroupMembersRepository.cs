using KTPS.Model.Entities.Groups;
using System.Collections.Generic;
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

    public async Task DeleteGroupMemberAsync(int userId, int groupId)
    {
        var sql = @"
            DELETE FROM group_members
            WHERE UserId = @UserID AND GroupId = @GroupID;";

        await _repository.ExecuteAsync<dynamic>(sql, new { UserID = userId, GroupID = groupId });
    }

    public async Task<IEnumerable<GroupMember>> GetByGroupIDAsync(int groupId)
    {
        var sql = @"
            SELECT ID, GroupID, UserID
            WHERE GroupId = @GroupID;";

        return await _repository.QueryListAsync<GroupMember, dynamic>(sql, new { GroupID = groupId });
    }
}