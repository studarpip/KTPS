using KTPS.Model.Entities.Registration;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Registration;

public interface IRegistrationRepository
{
    Task<int> InsertAsync(RegistrationBasic registration);
    Task<RegistrationBasic> GetByID(int id);
    Task AddUserToRegistration(int id, int userId);
}