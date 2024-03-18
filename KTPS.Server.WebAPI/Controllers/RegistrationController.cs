using KTPS.Model.Entities;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Services.Registration;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KTPS.Server.WebAPI.Controllers;

[Controller, Route("/register")]
public class RegistrationController
{
    private readonly IRegistrationService _registrationService;

    public RegistrationController(
        IRegistrationService registrationService
        )
    {
        _registrationService = registrationService;
    }

    [HttpPost("start")]
    public async Task<ServerResult<int>> StartAsync([FromBody] RegistrationStartRequest request) => await _registrationService.StartRegistrationAsync(request);

    [HttpPost("auth")]
    public async Task<ServerResult<int>> AuthAsync([FromBody] RegistrationAuthRequest request) => await _registrationService.AuthRegistrationAsync(request);
}