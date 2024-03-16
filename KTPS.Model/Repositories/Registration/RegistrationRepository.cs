using KTPS.Model.Entities.Registration;
using System;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Registration;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly IRepository _repository;

    public RegistrationRepository(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<int> InsertAsync(RegistrationBasic registration)
    {
        var sql = @"
            INSERT INTO registrations (`UserID`, `Username`, `Password`, `Email`, `AuthCode`)
            VALUES (@UserId, @Username, @Password, @Email, @AuthCode);
            SELECT LAST_INSERT_ID();";

        return await _repository.QueryAsync<int, dynamic>(sql, new
        {
            UserID = registration.UserID,
            Username = registration.Username,
            Password = registration.Password,
            Email = registration.Email,
            AuthCode = registration.AuthCode
        });
    }

    public async Task<RegistrationBasic> GetByID(int id) => throw new NotImplementedException();
}