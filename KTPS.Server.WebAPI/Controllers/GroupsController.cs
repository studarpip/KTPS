using KTPS.Model.Entities;
using KTPS.Model.Entities.Groups;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Entities.Responses;
using KTPS.Model.Services.Groups;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Server.WebAPI.Controllers;

[Controller, Route("groups")]
public class GroupsController
{
    private readonly IGroupsService _groupsService;

    public GroupsController(
        IGroupsService groupsService
        )
    {
        _groupsService = groupsService;
    }

    [HttpPost("new")]
    public async Task<ServerResult<int>> NewAsync([FromBody] NewGroupRequest request) => await _groupsService.NewGroupAsync(request);

    [HttpPost("edit")]
    public async Task<ServerResult> EditAsync([FromBody] EditGroupRequest request) => await _groupsService.EditGroupAsync(request);

    [HttpPost("delete")]
    public async Task<ServerResult> DeleteAsync([FromBody] DeleteGroupRequest request) => await _groupsService.DeleteGroupAsync(request);

    [HttpGet("users/{userId}/list")]
    public async Task<ServerResult<IEnumerable<GroupBasic>>> ListAsync(int userId) => await _groupsService.GetGroupListAsync(userId);

    [HttpPost("remove_members")]
    public async Task<ServerResult> RemoveMembersAsync([FromBody] RemoveGroupMembersRequest request) => await _groupsService.RemoveGroupMembersAsync(request);

    [HttpGet("{groupId}/members")]
    public async Task<ServerResult<GetGroupMembersResponse>> MemberListAsync(int groupId) => await _groupsService.GetMemberListAsync(groupId);

    [HttpPost("add_guest")]
    public async Task<ServerResult<int>> AddGuestAsync([FromBody] AddGuestRequest request) => await _groupsService.AddGuestAsync(request);
}