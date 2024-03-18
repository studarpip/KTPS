namespace KTPS.Model.Entities.Requests;

public class CreateNotificationRequest
{
    public int SenderID { get; set; }
    public int ReceiverID { get; set; }
    public string Type { get; set; }
}