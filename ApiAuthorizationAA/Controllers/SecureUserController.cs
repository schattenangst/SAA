
namespace ApiAuthorizationAA.Controllers
{
    using ApiAuthorizationAA.Model.Entities.User;
    using ApiAuthorizationAA.Service.SecureUser;
    using System.Threading.Tasks;
    using System.Web.Http;

    //[Authorize]
    /// <summary>
    /// Servicio para asegurar credenciales de usuario
    /// </summary>
    public class SecureUserController : ApiController
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserCreateHashService userCreateHashService;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCreateHashService"></param>
        public SecureUserController(IUserCreateHashService userCreateHashService)
        {
            this.userCreateHashService = userCreateHashService;
        }
        #endregion

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Post([FromBody]UserEntity userEntity)
        {
            //
            var response = await userCreateHashService.CreateNewUserHashPassword(userEntity);

            return Ok(response.Response);
        }


        [HttpPut]
        [Route("")]
        public IHttpActionResult Put()
        {
            return Ok();
        }


    }
}
