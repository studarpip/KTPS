using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Repositories.Notifications;
using KTPS.Model.Entities.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Notifications;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(
        INotificationRepository notificationRepository
        )
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<ServerResult<int>> CreateAsync(CreateNotificationRequest request)
    {
        try
        {
            var newNotification = new Notification() { SenderID = request.SenderID, ReceiverID = request.ReceiverID, Type = request.Type };
            var id = await _notificationRepository.InsertAsync(newNotification);
            return new() { Success = true, Data = id };
        }
        catch
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult<IEnumerable<Notification>>> ListAsync(int userId)
    {
        try
        {
            var notifications = await _notificationRepository.ListAsync(userId);
            return new() { Success = true, Data = notifications };
        }
        catch
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult> RespondAsync(RespondNotificationRequest request)
    {
        try
        {
            await _notificationRepository.RespondAsync(request);
            //handle request: add freind/group
            return new() { Success = true };
        }
        catch
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

}