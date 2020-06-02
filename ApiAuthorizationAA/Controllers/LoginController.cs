
namespace ApiAuthorizationAA.Controllers
{
    using ApiAuthotizationAA.Model.Entities.Cryptography;
    using Model.Entities.User;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class LoginController : ApiController
    {
        #region Fields
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptShaServices"></param>
        /// <param name="userService"></param>
        public LoginController()
        {
        }
        #endregion

        #region Actions
        [HttpGet]
        [Route("User")]
        public IHttpActionResult GetUser([FromBody]UserEntity user)
        {

            UserSecureEntity secureUser = new UserSecureEntity
            {
                Password = user.Password,
            };

            return Json(secureUser);
        }

        [HttpGet]
        [Route("UserEncrypt/{userName}/{password}")]
        public IHttpActionResult GetUserParams(string userName, string password)
        {
            //PasswordHashContainerEntity securePassword = encryptShaServices.CreateHash(password);

            UserSecureEntity secureUser = new UserSecureEntity
            {
                Password = password,
                //PasswordHash = Convert.ToBase64String(securePassword.HashedPassword),
                //Salt = Convert.ToBase64String(securePassword.Salt)
            };

            //byte[] encryptPassword = encryptShaServices.CreateHash(password, securePassword.Salt);

            //string encryptedPassword = Convert.ToBase64String(encryptPassword);

            //secureUser.PasswordHastTemp = encryptedPassword;

            return Json(secureUser);
        }

        //[HttpGet]
        //[Route("Users")]
        //public async Task<IHttpActionResult> GetUsers()
        //{
        //    var result = await userService.GetUsers();

        //    return Json(result);
        //}

        //[HttpPost]
        //[Route("CreateUserAsync")]
        //public async Task<IHttpActionResult> PostCreateUserAsync([FromBody] UserEntity newUser)
        //{
        //    var result = await userService.CreateUser(newUser);

        //    return Json(result);
        //}

        //[HttpPost]
        //[Route("CreateUserBody")]
        //public async Task<IHttpActionResult> PostCreateUserBody([FromBody] UserEntity newUser)
        //{
        //    var result = await userService.CreateUser(newUser);

        //    return Json(result);
        //}

        //[HttpPost]
        //[Route("CreateUser")]
        //public async Task<IHttpActionResult> PostCreateUser(UserEntity newUser)
        //{
        //    if (newUser == null)
        //    {
        //        return Json("Objeto nulo");
        //    }

        //    var result = await userService.CreateUser(newUser);

        //    return Json(result);
        //}

        [HttpGet]
        [Route("Test")]
        public async Task<IHttpActionResult> GetTestResponse(string idUser)
        {
            await Task.Delay(1000);
            return Json($"Hola {idUser}");
        }

        [HttpGet]
        [Route("TestBody")]
        public async Task<IHttpActionResult> GetTestResponseBody([FromBody]string idUser)
        {
            await Task.Delay(1000);
            return Json($"Hola {idUser}");
        }

        [HttpPut]
        [Route("PutTest")]
        public IHttpActionResult PutUser(UserEntity user)
        {
            return Json("OK");
        }
        #endregion
    }
}
