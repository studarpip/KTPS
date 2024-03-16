using KTPS.Model.Entities.User;
using System;
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

	public async Task<UserBasic> GetByEmailAsync(string email) => throw new NotImplementedException();

	public async Task InsertAsync(UserBasic user) => throw new NotImplementedException();

	public Task<Task<UserBasic>> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	Task<UserBasic> IUserRepository.GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<UserBasic> UpdateUserAsync(UserBasic updatedUser)
	{
		throw new NotImplementedException();
	}
}