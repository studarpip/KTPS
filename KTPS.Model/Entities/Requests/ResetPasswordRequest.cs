namespace KTPS.Model.Entities.Requests;

public class ResetPasswordRequest
{
    public int UserID { get; set; }
    public string NewPassword { get; set; }
    public string AuthCheck { get; set; }
}