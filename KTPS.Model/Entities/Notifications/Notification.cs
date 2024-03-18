namespace KTPS.Model.Entities.Notifications;

public class Notification
{
    public int ID { get; set; }
    public int SenderID { get; set; }
    public int ReceiverID { get; set; }
    public int? GroupID { get; set; }
    public string Type { get; set; }
    public bool responded { get; set; }
}