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

	public async Task<UserBasic> GetByUsernameAsync(string username) => throw new NotImplementedException();

	public async Task<UserBasic> GetByEmailAsync(string email) => throw new NotImplementedException();

	public async Task InserAsync(UserBasic user) => throw new NotImplementedException();
}