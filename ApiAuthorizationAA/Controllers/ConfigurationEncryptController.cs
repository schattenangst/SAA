
namespace ApiAuthorizationAA.Controllers
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Entities.EncryptConfiguration;
    using ApiAuthorizationAA.Service.EncryptConfiguration;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    //[Authorize]
    [RoutePrefix("configuration/encrypt")]
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationEncryptController : ApiController
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private readonly IControlEncryptPersistence controlEncryptService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor controller
        /// </summary>
        /// <param name="controlEncryptService"></param>
        public ConfigurationEncryptController(IControlEncryptPersistence controlEncryptService)
        {
            this.controlEncryptService = controlEncryptService;
        }
        #endregion

        #region Actions
        [HttpGet]
        //[Route("get/configuration")]
        public async Task<IHttpActionResult> GetEncryptConfiguration()
        {
            ResponseDto<ControlEncryptEntity> response = await controlEncryptService.GetCurrentControlEncryptAsync();

            if (response != null && !response.IsError)
            {
                return Ok(response.Response);
            }
            else
            {
                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, response.ErrorMessage);
                return ResponseMessage(responseMessage);
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> PostEncryptConfiguration(ControlEncryptEntity controlEncryptEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            ResponseDto<bool> response = await controlEncryptService.CreateConfigurationAsync(controlEncryptEntity);

            if (response != null && !response.IsError)
            {
                return Created("success", response.Response);
            }
            else
            {
                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, response.ErrorMessage);
                return ResponseMessage(responseMessage);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IHttpActionResult> PutEncryptConfiguration(int idControlEncryptEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            ResponseDto<bool> response = await controlEncryptService.DisableConfigurationByIdAsync(idControlEncryptEntity);

            if (response != null && !response.IsError)
            {
                return StatusCode(HttpStatusCode.Accepted);
            }
            else
            {
                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, response.ErrorMessage);
                return ResponseMessage(responseMessage);
            }
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
