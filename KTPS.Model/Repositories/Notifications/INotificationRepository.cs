using KTPS.Model.Entities.Notifications;
using KTPS.Model.Entities.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Notifications;

public interface INotificationRepository
{
    public Task<int> InsertAsync(Notification notification);
    public Task<IEnumerable<Notification>> ListAsync(int userId);
    public Task RespondAsync(RespondNotificationRequest request);
    public Task<Notification> GetAsync(int notificationId);
}