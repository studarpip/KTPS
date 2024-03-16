using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Services.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KTPS.Server.WebAPI.Controllers;

[Controller]
public class LoginController
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [Route("/login")]
    public async Task<ServerResult<int>> StartAsync([FromBody] LoginRequest request) => await _loginService.LoginAsync(request);

}