using System.Collections.Generic;

namespace KTPS.Model.Entities.Requests;

public class RemoveGroupMembersRequest
{
    public List<int> UserIDList { get; set; }
    public int GroupID { get; set; }
    public int OwnerUserID { get; set; }
}