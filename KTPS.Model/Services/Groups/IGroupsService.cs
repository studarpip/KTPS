using KTPS.Model.Entities;
using KTPS.Model.Entities.Groups;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Entities.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Groups;

public interface IGroupsService
{
    Task<ServerResult<int>> NewGroupAsync(NewGroupRequest request);
    Task<ServerResult> EditGroupAsync(EditGroupRequest request);
    Task<ServerResult> DeleteGroupAsync(DeleteGroupRequest request);
    Task<ServerResult<IEnumerable<GroupBasic>>> GetGroupListAsync(int userId);
    Task<ServerResult> RemoveGroupMembersAsync(RemoveGroupMembersRequest request);
    Task<ServerResult<GetGroupMembersResponse>> GetMemberListAsync(int groupId);
    Task<ServerResult<int>> AddGuestAsync(AddGuestRequest request);
    Task<ServerResult> AddGroupMemberAsync(int groupId, int userId);
}