using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Registration;

public interface IRegistrationService
{
    Task<ServerResult<int>> StartRegistrationAsync(RegistrationStartRequest request);
    Task<ServerResult<int>> AuthRegistrationAsync(RegistrationAuthRequest request);
}