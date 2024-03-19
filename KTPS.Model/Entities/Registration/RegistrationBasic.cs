namespace KTPS.Model.Entities.Registration;
public class RegistrationBasic
{
    public int ID { get; set; }
    public int? UserID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string AuthCode { get; set; }
}