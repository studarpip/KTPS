using KTPS.Model.Entities;
using KTPS.Model.Entities.Notifications;
using KTPS.Model.Entities.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Notifications;

public interface INotificationService
{
    public Task<ServerResult<int>> CreateAsync(CreateNotificationRequest request);
    public Task<ServerResult<IEnumerable<Notification>>> ListAsync(int userId);
    public Task<ServerResult> RespondAsync(RespondNotificationRequest request);
}