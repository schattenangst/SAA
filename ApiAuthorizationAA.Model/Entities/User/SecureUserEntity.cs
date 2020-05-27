
namespace ApiAuthorizationAA.Model.Entities.User
{
    public class SecureUserEntity
    {
        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string PasswordHastTemp { get; set; }
    }
}