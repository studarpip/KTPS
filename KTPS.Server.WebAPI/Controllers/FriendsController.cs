using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Entities.User;
using KTPS.Model.Services.Friends;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Server.WebAPI.Controllers;

[Controller, Route("friends")]
public class FriendsController
{
	private readonly IFriendsService _friendsService;

	public FriendsController(
		IFriendsService friendsService
		)
	{
		_friendsService = friendsService;
	}

	[HttpGet("list/{userId}")]
	public async Task<ServerResult<IEnumerable<UserMinimal>>> FriendListAsync(int userId) => await _friendsService.GetFriendListAsync(userId);

    [HttpPost("delete")]
    public async Task<ServerResult> DeleteFriendAsync([FromBody] DeleteFriendRequest request) => await _friendsService.DeleteFriendAsync(request);

    [HttpPost("find")]
    public async Task<ServerResult<IEnumerable<UserMinimal>>> FindFriendAsync([FromBody] FindFriendRequest request) => await _friendsService.FindFriendAsync(request);

	[HttpPost("add")]
	public async Task<ServerResult> AddFriendAsync([FromBody] AddFriendRequest request) => await _friendsService.AddFriendAsync(request);
}