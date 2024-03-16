using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Services.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KTPS.Server.WebAPI.Controllers;

[Controller, Route("/login")]
public class LoginController
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<ServerResult<int>> Login([FromBody] LoginRequest request) => await _loginService.LoginAsync(request);

    [HttpPost("forgotMyPassword")]
    public async Task<ServerResult<int>> ForgotMyPassword([FromBody] ForgotPasswordRequest request) => await _loginService.ForgotPasswordAsync(request);

    [HttpPost("resetAuth")]
    public async Task<ServerResult> ResetAuth([FromBody] ResetPasswordAuthRequest request) => await _loginService.ResetPasswordAuthAsync(request);

    [HttpPost("reset")]
    public async Task<ServerResult> Reset([FromBody] ResetPasswordRequest request) => await _loginService.ResetPasswordAsync(request);

}