namespace KTPS.Model.Entities.Groups;

public class GroupMember
{
    public int ID { get; set; }
    public int GroupID { get; set; }
    public int UserID { get; set; }
}