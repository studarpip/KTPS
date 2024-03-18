using KTPS.Model.Entities;
using KTPS.Model.Entities.Notifications;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Repositories.Notifications;
using KTPS.Model.Services.Friends;
using KTPS.Model.Services.Groups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Notifications;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IFriendsService _friendsService;
    private readonly IGroupsService _groupsService;

    public NotificationService(
        INotificationRepository notificationRepository,
        IFriendsService friendsService,
        IGroupsService groupsService
        )
    {
        _notificationRepository = notificationRepository;
        _friendsService = friendsService;
        _groupsService = groupsService;
    }

    public async Task<ServerResult<int>> CreateAsync(CreateNotificationRequest request)
    {
        try
        {
            var newNotification = new Notification() { SenderID = request.SenderID, ReceiverID = request.ReceiverID, Type = request.Type, GroupID = request.GroupId };
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
            var notification = await _notificationRepository.GetAsync(request.NotificationID);

            if(!request.Accept)
                return new() { Success = true };

            switch (notification.Type)
            {
                case "Friend":
                    {
                        await _friendsService.AddFriendAsync(notification.SenderID, notification.ReceiverID);
                        break;
                    }
                case "Group":
                    {
                        await _groupsService.AddGroupMemberAsync((int)notification.GroupID, notification.ReceiverID);
                        break;
                    }
                default: break;
            }

            return new() { Success = true };
        }
        catch
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

}