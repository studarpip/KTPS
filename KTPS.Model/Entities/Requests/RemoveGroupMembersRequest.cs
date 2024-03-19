using System.Collections.Generic;

namespace KTPS.Model.Entities.Requests;

public class RemoveGroupMembersRequest
{
    public int? UserToRemoveID { get; set; }
    public int? GuestToRemoveID { get; set; }
    public int GroupID { get; set; }
    public int RequestUserID { get; set; }
}