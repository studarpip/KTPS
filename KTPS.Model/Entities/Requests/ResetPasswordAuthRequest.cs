namespace KTPS.Model.Entities.Requests;

public class ResetPasswordAuthRequest
{
    public int UserID { get; set; }
    public string RecoveryCode { get; set; }
}