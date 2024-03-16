using KTPS.Model.Entities.Guests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.Guests;

public interface IGuestsRepository
{
    Task<IEnumerable<Guest>> GetByGroupID(int groupId);
    Task<int> InsertAsync(Guest guest);
}