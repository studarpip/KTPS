namespace KTPS.Model.Entities.Requests;

public class RegistrationAuthRequest
{
    public int RegistrationID { get; set; }
    public string AuthCode { get; set; }
}