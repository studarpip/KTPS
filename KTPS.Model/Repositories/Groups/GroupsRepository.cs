using KTPS.Model.Entities.Groups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Groups;

public class GroupsRepository : IGroupsRepository
{
    private readonly IRepository _repository;

    public GroupsRepository(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GroupBasic>> GetUserGroupsAsync(int userId)
    {
        var sql = @"
            SELECT ID, Name, OwnerUserID
            FROM groups
            WHERE OwnerUserID = @OwnerUserID;";

        return await _repository.QueryListAsync<GroupBasic, dynamic>(sql, new { OwnerUserID = userId });
    }

    public async Task<GroupBasic> GetGroupAsync(int id)
    {
        var sql = @"
            SELECT ID, Name, OwnerUserID
            FROM groups
            WHERE ID = @Id;";

        return await _repository.QueryAsync<GroupBasic, dynamic>(sql, new { Id = id });
    }

    public async Task<int> InsertAsync(GroupBasic group)
    {
        var sql = @"
            INSERT INTO groups (`Name`, `OwnerUserID`)
            VALUES (@Name, @OwnerUserID);
            SELECT LAST_INSERT_ID();";

        return await _repository.QueryAsync<int, dynamic>(sql, new { Name = group.Name, OwnerUserId = group.OwnerUserID });
    }

    public async Task UpdateAsync(GroupBasic group)
    {
        var sql = @"
            UPDATE groups
            SET Name = @Name, OwnerUserID = @OwnerUserID
            WHERE ID = @Id";

        await _repository.ExecuteAsync<dynamic>(sql, new { Id = group.ID, Name = group.Name, OwnerUserId = group.OwnerUserID });
    }

    public async Task DeleteAsync(int id)
    {
        var sql = @"
            DELETE FROM groups
            WHERE ID = @Id";

        await _repository.ExecuteAsync<dynamic>(sql, new { Id = id });
    }
}