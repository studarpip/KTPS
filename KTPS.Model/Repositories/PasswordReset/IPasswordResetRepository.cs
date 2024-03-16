using System.Threading.Tasks;

namespace KTPS.Model.Repositories.PasswordReset;

public interface IPasswordResetRepository
{
    Task InsertCodeAsync(int userId, string recoveryCode);
    Task<string> GetCodeAsync(int userId);
}