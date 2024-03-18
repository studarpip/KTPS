using KTPS.Model.Entities.Guests;
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
        //TODO
        var sql = @"
            INSERT INTO notifications (`SenderID`, `ReceiverID`, `Type`, `Responded`)
            VALUES(@SenderID, @ReceiverID, @Type, @Responded);
            SELECT LAST_INSERT_ID();";

        return await _repository.QueryAsync<int, dynamic>(sql, new
        {
            SenderID = notification.SenderID,
            ReceiverID = notification.ReceiverID,
            Type = notification.Type,
            Responded = false
        });
    }

    public async Task<IEnumerable<Notification>> ListAsync(int userId)
    {
        //TODO
        var sql = @"SELECT * FROM notifications WHERE SenderID = @UserID";
        return await _repository.QueryListAsync<Notification, dynamic>(sql, new { UserID = userId });
    }

    public async Task RespondAsync(RespondNotificationRequest request)
    {
        //TODO
        var sql = @"UPDATE notifications SET Responded = @Responded WHERE Id = @Id";
        await _repository.ExecuteAsync(sql, new { Responded = true, Id = request.NotificationID });
    }
}