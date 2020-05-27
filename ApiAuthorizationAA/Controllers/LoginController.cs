
namespace ApiAuthorizationAA.Controllers
{
    using ApiAuthotizationAA.Model.Entities.Cryptography;
    using Common.IService.Crypography;
    using Common.IService.User;
    using Model.Entities.User;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class LoginController : ApiController
    {
        private readonly IEncryptShaServices encryptShaServices;
        private readonly IUserService userService;

        public LoginController(IEncryptShaServices encryptShaServices)
        {
            this.encryptShaServices = encryptShaServices;
        }


        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        public LoginController(IEncryptShaServices encryptShaServices, IUserService userService)
        {
            this.encryptShaServices = encryptShaServices;
            this.userService = userService;
        }

        #region Actions

        [HttpGet]
        [Route("User")]
        public IHttpActionResult GetUser(UserEntity user)
        {
            PasswordHashContainerEntity securePassword = encryptShaServices.CreateHash(user.Password);

            SecureUserEntity secureUser = new SecureUserEntity
            {
                Password = user.Password,
                PasswordHash = Convert.ToBase64String(securePassword.HashedPassword),
                Salt = Convert.ToBase64String(securePassword.Salt)
            };

            byte[] encryptPassword = encryptShaServices.CreateHash(user.Password, securePassword.Salt);

            string encryptedPassword = Convert.ToBase64String(encryptPassword);

            secureUser.PasswordHastTemp = encryptedPassword;

            return Json(secureUser);
        }

        [HttpGet]
        [Route("Users")]
        public async Task<IHttpActionResult> GetUsers()
        {
            var result = await userService.GetUsers();

            return Json(result);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IHttpActionResult> PostCreateUser([FromBodyAttribute] UserEntity newUser)
        {
            var result = await userService.CreateUser(newUser);

            return Json(result);
        }
        #endregion
    }
}
