
namespace ApiAuthorizationAA.Controllers
{
    using Common.Dto;
    using Model.Entities.User;
    using Service.SecureUser;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    //[Authorize]
    /// <summary>
    /// Servicio para asegurar credenciales de usuario
    /// </summary>
    [RoutePrefix("secure/user")]
    public class SecureUserController : ApiController
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserHashService userCreateHashService;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCreateHashService"></param>
        public SecureUserController(IUserHashService userCreateHashService)
        {
            this.userCreateHashService = userCreateHashService;
        }
        #endregion

        #region Actions
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("validate/{user}/{password}")]
        public IHttpActionResult Get(string user, string password)
        {
            return Ok("usuario válido");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Post([FromBody]UserEntity userEntity)
        {
            if (ModelState.IsValid)
            {
                // 
                var response = await userCreateHashService.CreateNewUserHashPassword(userEntity);

                // 
                if (response != null && !response.IsError)
                {
                    return Created("create info success", response.Response);
                }
                else
                {
                    HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, response.ErrorMessage);
                    return ResponseMessage(responseMessage);
                }
            }
            else
            {
                return BadRequest("Not a valid model");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public async Task<IHttpActionResult> Put([FromBody] UserEntity userEntity)
        {
            if (ModelState.IsValid)
            {
                // 
                ResponseDto<bool> response = await userCreateHashService.UpdateUserHashPassword(userEntity);

                // 
                if (response != null && !response.IsError)
                {
                    HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.Accepted, response.Response);
                    return ResponseMessage(responseMessage);
                }
                else
                {
                    HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, response.ErrorMessage);
                    return ResponseMessage(responseMessage);
                }
            }
            else
            {
                return BadRequest("Not a valid model");
            }
        }
        #endregion
    }
}
