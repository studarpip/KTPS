using KTPS.Model.Entities.Groups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.GroupMembers;

public interface IGroupMembersRepository
{
    Task DeleteGroupGuestAsync(int guestId, int iD);
    Task DeleteGroupMemberAsync(int userId, int groupId);
    Task<IEnumerable<GroupMember>> GetByGroupIDAsync(int groupId);
    Task AddGroupMemberAsync(int userId, int groupId);
}