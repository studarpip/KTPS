using System.Collections.Generic;

namespace KTPS.Model.Entities.Requests;

public class RemoveGroupMembersRequest
{
    public int? UserToRemoveId { get; set; }
    public int? GuestToRemoveId { get; set; }
    public int GroupID { get; set; }
    public int RequestUserId { get; set; }
}