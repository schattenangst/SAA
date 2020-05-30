
namespace ApiAuthorizationAA.Persistence.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using System.Threading.Tasks;

    public interface IControlEncryptPersistence
    {
        /// <summary>
        /// Insert new configuration to encrypt 
        /// </summary>
        /// <param name="controlEncrypt">Object <see cref="ControlEncrypt"/> with info</param>
        /// <returns></returns>
        Task<ResponseDto<bool>> CreateConfigurationAsync(ControlEncrypt controlEncrypt);

        /// <summary>
        /// Update configuration only active or inactive
        /// </summary>
        /// <returns></returns>
        Task<ResponseDto<bool>> DisableConfigurationAllAsync();

        /// <summary>
        /// Update configuration only active or inactive
        /// </summary>
        /// <param name="idControlEncrypt">Key configuration</param>
        /// <returns></returns>
        Task<ResponseDto<bool>> DisableConfigurationByIdAsync(int idControlEncrypt);

        /// <summary>
        /// Get last active configuration encrypt
        /// </summary>
        /// <returns></returns>
        Task<ResponseDto<ControlEncrypt>> GetCurrentControlEncryptAsync();

        /// <summary>
        /// Get configuration encrypt by Id
        /// </summary>
        /// <param name="IdControlEncrypt"></param>
        /// <returns></returns>
        Task<ResponseDto<ControlEncrypt>> GetControlEncryptByIdAsync(int IdControlEncrypt);
    }
}
