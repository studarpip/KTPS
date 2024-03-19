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

            var ID = await _groupsRepository.InsertAsync(new()
            {
                Name = request.Name,
                OwnerUserID = request.UserID
            });

            await _groupMembersRepository.AddGroupMemberAsync(request.UserID, ID);

            return new() { Success = true, Data = ID };
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
            var groupMembers = (await _groupMembersRepository.GetByGroupIDAsync(group.ID)).ToList();
            foreach (var member in groupMembers)
                await _groupMembersRepository.DeleteGroupMemberAsync(member.UserID, group.ID);
            return new() { Success = true };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult<IEnumerable<GroupBasic>>> GetGroupListAsync(int userID)
    {
        try
        {
            var list = await _groupsRepository.GetUserGroupsAsync(userID);
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

            if (!group.OwnerUserID.Equals(request.RequestUserID))
                return new() { Success = false, Message = "Only the owner can remove group members!" };

            if (request.UserToRemoveID != null && request.UserToRemoveID != group.OwnerUserID)
                await _groupMembersRepository.DeleteGroupMemberAsync((int)request.UserToRemoveID, group.ID);

            if (request.GuestToRemoveID != null)
                await _groupMembersRepository.DeleteGroupGuestAsync((int)request.GuestToRemoveID, group.ID);

            return new() { Success = true };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult<GetGroupMembersResponse>> GetMemberListAsync(int groupID)
    {
        try
        {
            var group = await _groupsRepository.GetGroupAsync(groupID);
            if (group is null)
                return new() { Success = false, Message = "Group does not exist!" };

            var members = await _groupMembersRepository.GetByGroupIDAsync(groupID);
            var guests = await _guestsRepository.GetByGroupID(groupID);

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

            var ID = await _guestsRepository.InsertAsync(new() { GroupID = request.GroupID, Name = request.Name });
            return new() { Success = true, Data = ID };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult> AddGroupMemberAsync(int groupID, int userID)
    {
        try
        {
            await _groupMembersRepository.AddGroupMemberAsync(userID, groupID);
            return new() { Success = true };
        }
        catch (Exception)
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }
}