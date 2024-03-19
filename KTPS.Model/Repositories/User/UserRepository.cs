using KTPS.Model.Entities.User;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.User;

public class UserRepository : IUserRepository
{
	private readonly IRepository _repository;

	public UserRepository(
		IRepository repository
		)
	{
		_repository = repository;
	}

	public async Task<UserBasic> GetByUsernameAsync(string username)
	{
		var query = @"SELECT * FROM users WHERE Username = @Username";

		return await _repository.QueryAsync<UserBasic, dynamic>(query, new { Username = username });
	}

	public async Task<UserBasic> GetByEmailAsync(string email)
	{
		var sql = @"SELECT * FROM users WHERE Email = @Email";

		return await _repository.QueryAsync<UserBasic, dynamic>(sql, new { Email = email });
	}

	public async Task<int> InsertAsync(UserBasic user)
	{
        var sql = @"
            INSERT INTO users (`Username`, `Password`, `Email`)
            VALUES (@Username, @Password, @Email);
            SELECT LAST_INSERT_ID();";

        return await _repository.QueryAsync<int, dynamic>(sql, new
        {
            Username = user.Username,
            Password = user.Password,
            Email = user.Email
        });
    }

	public async Task<UserBasic> GetByIdAsync(int id)
	{
        var sql = @"SELECT * FROM users WHERE ID = @Id";

        return await _repository.QueryAsync<UserBasic, dynamic>(sql, new { Id = id });
    }

	public async Task UpdateUserAsync(UserBasic updatedUser)
	{
        var sql = @"
            UPDATE users SET
            Username = @Username,
            Password = @Password,
            Email = @Email
            WHERE ID = @Id";

        await _repository.ExecuteAsync<dynamic>(sql, new
        {
            Username = updatedUser.Username,
            Password = updatedUser.Password,
            Email = updatedUser.Email,
            Id = updatedUser.ID
        });
    }
}