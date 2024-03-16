using KTPS.Model.Entities.Groups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Groups;

public interface IGroupsRepository
{
    Task<IEnumerable<GroupBasic>> GetUserGroupsAsync(int userId);
    Task<GroupBasic> GetGroupAsync(int id);
    Task<int> InsertAsync(GroupBasic group);
    Task UpdateAsync(GroupBasic group);
    Task DeleteAsync(int id);
}