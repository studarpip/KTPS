using KTPS.Model.Entities.Groups;
using KTPS.Model.Entities.Guests;
using System.Collections.Generic;

namespace KTPS.Model.Entities.Responses;

public class GetGroupMembersResponse
{
    public int OwnerUserID { get; set; }
    public List<GroupMember> Members { get; set; }
    public List<Guest> Guests { get; set; }
}