using KTPS.Model.Entities.Notifications;
using KTPS.Model.Entities.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Notifications;

public class NotificationRepository : INotificationRepository
{
    private readonly IRepository _repository;

    public NotificationRepository(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<int> InsertAsync(Notification notification)
    {
        var sql = @"
            INSERT INTO notifications (`SenderID`, `ReceiverID`, `GroupID`, `Type`, `Responded`)
            VALUES(@SenderID, @ReceiverID, @GroupID, @Type, @Responded);
            SELECT LAST_INSERT_ID();";

        return await _repository.QueryAsync<int, dynamic>(sql, new
        {
            SenderID = notification.SenderID,
            ReceiverID = notification.ReceiverID,
            GroupID = notification.GroupID,
            Type = notification.Type,
            Responded = false
        });
    }

    public async Task<IEnumerable<Notification>> ListAsync(int userId)
    {
        var sql = @"SELECT * FROM notifications WHERE ReceiverID = @UserID";
        return await _repository.QueryListAsync<Notification, dynamic>(sql, new { UserID = userId });
    }

    public async Task<Notification> GetAsync(int notificationId)
    {
        var sql = @"SELECT * FROM notifications WHERE ID = @ID LIMIT 1";
        return await _repository.QueryAsync<Notification, dynamic>(sql, new { ID = notificationId });
    }

    public async Task RespondAsync(RespondNotificationRequest request)
    {
        var sql = @"UPDATE notifications SET Responded = @Responded WHERE ID = @ID";
        await _repository.ExecuteAsync(sql, new { Responded = true, ID = request.NotificationID });
    }
}