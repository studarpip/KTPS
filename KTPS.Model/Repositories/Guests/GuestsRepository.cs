using KTPS.Model.Entities.Guests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Guests;

public class GuestsRepository : IGuestsRepository
{
    private readonly IRepository _repository;

    public GuestsRepository(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Guest>> GetByGroupID(int groupId)
    {
        var sql = @"
            SELECT ID, Name, GroupID FROM guests
            WHERE GroupID = @GroupID;";

        return await _repository.QueryListAsync<Guest, dynamic>(sql, new { GroupID = groupId });
    }

    public async Task<int> InsertAsync(Guest guest)
    {
        var sql = @"
            INSERT INTO guests (`Name`, `GroupID`)
            VALUES(@Name, @GroupID);
            SELECT LAST_INSERT_ID();";

        return await _repository.QueryAsync<int, dynamic>(sql, new { Name = guest.Name, GroupID = guest.GroupID });
    }
}