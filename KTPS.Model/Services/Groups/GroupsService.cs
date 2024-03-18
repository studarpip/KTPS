using KTPS.Model.Entities;
using KTPS.Model.Entities.Groups;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Entities.Responses;
using KTPS.Model.Repositories.GroupMembers;
using KTPS.Model.Repositories.Groups;
using KTPS.Model.Repositories.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Groups;

public class GroupsService : IGroupsService
{
    private readonly IGroupsRepository _groupsRepository;
    private readonly IGroupMembersRepository _groupMembersRepository;
    private readonly IGuestsRepository _guestsRepository;

    public GroupsService(
        IGroupsRepository groupsRepository,
        IGroupMembersRepository groupMembersRepository,
        IGuestsRepository guestsRepository
        )
    {
        _groupsRepository = groupsRepository;
        _groupMembersRepository = groupMembersRepository;
        _guestsRepository = guestsRepository;
    }

    public async Task<ServerResult<int>> NewGroupAsync(NewGroupRequest request)
    {
        try
        {
            var userGroups = await _groupsRepository.GetUserGroupsAsync(request.UserID);
            if (userGroups.Any(x => x.Name == request.Name))
                return new() { Success = true, Message = $"Group with name {request.Name} already exists!" };

            var id = await _groupsRepository.InsertAsync(new()
            {
                Name = request.Name,
                OwnerUserID = request.UserID
            });

            await _groupMembersRepository.AddGroupMemberAsync(request.UserID, id);

            return new() { Success = true, Data = id };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult> EditGroupAsync(EditGroupRequest request)
    {
        try
        {
            var group = await _groupsRepository.GetGroupAsync(request.ID);
            if (group is null)
                return new() { Success = false, Message = "Group does not exist!" };

            if (!group.OwnerUserID.Equals(request.UserID))
                return new() { Success = false, Message = "Only the owner can edit the group name!" };

            group.Name = request.Name;
            await _groupsRepository.UpdateAsync(group);

            return new() { Success = true };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult> DeleteGroupAsync(DeleteGroupRequest request)
    {
        try
        {
            var group = await _groupsRepository.GetGroupAsync(request.ID);
            if (group is null)
                return new() { Success = false, Message = "Group does not exist!" };

            if (!group.OwnerUserID.Equals(request.UserID))
                return new() { Success = false, Message = "Only the owner can delete the group!" };

            await _groupsRepository.DeleteAsync(request.ID);
            return new() { Success = true };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult<IEnumerable<GroupBasic>>> GetGroupListAsync(int userId)
    {
        try
        {
            var list = await _groupsRepository.GetUserGroupsAsync(userId);
            return new() { Success = true, Data = list };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult> RemoveGroupMembersAsync(RemoveGroupMembersRequest request)
    {
        try
        {
            var group = await _groupsRepository.GetGroupAsync(request.GroupID);
            if (group is null)
                return new() { Success = false, Message = "Group does not exist!" };

            if (!group.OwnerUserID.Equals(request.RequestUserId))
                return new() { Success = false, Message = "Only the owner can remove group members!" };

            if (request.UserToRemoveId != null && request.UserToRemoveId != group.OwnerUserID)
                await _groupMembersRepository.DeleteGroupMemberAsync((int)request.UserToRemoveId, group.ID);

            if (request.GuestToRemoveId != null)
                await _groupMembersRepository.DeleteGroupGuestAsync((int)request.GuestToRemoveId, group.ID);

            return new() { Success = true };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult<GetGroupMembersResponse>> GetMemberListAsync(int groupId)
    {
        try
        {
            var group = await _groupsRepository.GetGroupAsync(groupId);
            if (group is null)
                return new() { Success = false, Message = "Group does not exist!" };

            var members = await _groupMembersRepository.GetByGroupIDAsync(groupId);
            var guests = await _guestsRepository.GetByGroupID(groupId);

            return new() { Success = true, Data = new() { Guests = guests.ToList(), Members = members.ToList(), OwnerUserID = group.OwnerUserID } };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult<int>> AddGuestAsync(AddGuestRequest request)
    {
        try
        {
            var group = await _groupsRepository.GetGroupAsync(request.GroupID);
            if (group is null)
                return new() { Success = false, Message = "Group does not exist!" };

            var id = await _guestsRepository.InsertAsync(new() { GroupID = request.GroupID, Name = request.Name });
            return new() { Success = true, Data = id };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult> AddGroupMemberAsync(int groupId, int userId)
    {
        try
        {
            await _groupMembersRepository.AddGroupMemberAsync(userId, groupId);
            return new() { Success = true };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }
}