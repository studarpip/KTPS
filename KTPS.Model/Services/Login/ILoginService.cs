using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Login;

public interface ILoginService
{
    Task<ServerResult<int>> LoginAsync(LoginRequest request);
    Task<ServerResult<int>> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<ServerResult> ResetPasswordAuthAsync(ResetPasswordAuthRequest request);
    Task<ServerResult> ResetPasswordAsync(ResetPasswordRequest request);
}