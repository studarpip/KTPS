namespace KTPS.Model.Entities.Requests;

public class RespondNotificationRequest
{
    public int NotificationID { get; set; }
    public int UserID { get; set; }
    public bool Accept { get; set; }
}