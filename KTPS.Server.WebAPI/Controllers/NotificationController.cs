using KTPS.Model.Entities;
using KTPS.Model.Entities.Notifications;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Services.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Server.WebAPI.Controllers;

[Controller, Route("notification")]
public class NotificationController
{
    private readonly INotificationService _notificationService;

    public NotificationController(
        INotificationService notificationService
        )
    {
        _notificationService = notificationService;
    }

    [HttpPost("create")]
    public async Task<ServerResult<int>> Create([FromBody] CreateNotificationRequest request) => await _notificationService.CreateAsync(request);

    [HttpGet("list/{userId}")]
    public async Task<ServerResult<IEnumerable<Notification>>> List(int userId) => await _notificationService.ListAsync(userId);

    [HttpPost("respond")]
    public async Task<ServerResult> Login([FromBody] RespondNotificationRequest request) => await _notificationService.RespondAsync(request);



}