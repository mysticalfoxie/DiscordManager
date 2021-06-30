namespace DCM.Models
{
    public class LoginCredentials
    {
        public LoginCredentials(string loginToken)
        {
            LoginToken = loginToken;
        }

        public string LoginToken { get; set; }
    }
}
