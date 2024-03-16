﻿using System.Threading.Tasks;

namespace KTPS.Model.Repositories.PasswordReset;

public class PasswordResetRepository : IPasswordResetRepository
{
	private readonly IRepository _repository;

	public PasswordResetRepository(
		IRepository repository
		)
	{
		_repository = repository;
	}

	public async Task<string> GetCodeAsync(int userId)
	{
		var query = @"
			SELECT RecoveryCode FROM passwordResets WHERE UserId = @UserID
			ORDER BY id DESC SELECT 1
			";

		return await _repository.QueryAsync<string, dynamic>(query, new { UserID = userId });
	}

	public async Task InsertCodeAsync(int userId, string recoveryCode)
	{
		var query = @"
            INSERT INTO passwordResets (`UserID`, `RecoveryCode`)
            VALUES (@UserId, @RecoveryCode);
			";

		await _repository.ExecuteAsync<dynamic>(query, new { UserID = userId, RecoveryCode = recoveryCode });
	}
}