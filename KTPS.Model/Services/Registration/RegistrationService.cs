using KTPS.Model.Entities;
using KTPS.Model.Entities.Registration;
using KTPS.Model.Entities.Requests;
using KTPS.Model.Repositories.Registration;
using KTPS.Model.Services.User;
using System;
using System.Threading.Tasks;

namespace KTPS.Model.Services.Registration;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IUserService _userService;

    public RegistrationService(
        IRegistrationRepository registrationRepository,
        IUserService userService
        )
    {
        _registrationRepository = registrationRepository;
        _userService = userService;
    }

    public async Task<ServerResult<int>> StartRegistrationAsync(RegistrationStartRequest request)
    {
        try
        {
            var emailExists = await _userService.EmailExistsAsync(request.Email);
            if (emailExists)
                return new() { Success = false, Message = "Email already exists!" };

            var usernameExists = await _userService.UsernameExistsAsync(request.Username);
            if (usernameExists)
                return new() { Success = false, Message = "Username already exists!" };

            var id = await _registrationRepository.InsertAsync(new RegistrationBasic
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
                AuthCode = "fsdfsd"
            });

            return new() { Success = true, Data = id };
        }
        catch
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    public async Task<ServerResult> AuthRegistrationAsync(RegistrationAuthRequest request)
    {
        try
        {
            var registration = await _registrationRepository.GetByID(request.RegistrationID);
            if (registration is null)
                return new() { Success = false, Message = "Registration does not exist!" };

            if (!registration.AuthCode.Equals(request.AuthCode))
                return new() { Success = false, Message = "Authentication code is incorrect!" };

            await _userService.CreateUserAsync(registration);

            return new() { Success = true };
        }
        catch
        {
            return new() { Success = false, Message = "Technical error!" };
        }
    }

    private string CreateAuthCode() => throw new NotImplementedException();
}