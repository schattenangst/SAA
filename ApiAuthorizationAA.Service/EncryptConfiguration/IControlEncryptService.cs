
namespace ApiAuthorizationAA.Service.EncryptConfiguration
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Entities.EncryptConfiguration;
    using System.Threading.Tasks;

    public interface IControlEncryptService
    {
        /// <summary>
        /// Insert new configuration to encrypt 
        /// </summary>
        /// <param name="controlEncrypt">Object <see cref="ControlEncryptEntity"/> with info</param>
        /// <returns></returns>
        Task<ResponseDto<bool>> CreateConfigurationAsync(ControlEncryptEntity controlEncrypt);

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
        Task<ResponseDto<ControlEncryptEntity>> GetCurrentControlEncryptAsync();

        /// <summary>
        /// Get configuration encrypt by Id
        /// </summary>
        /// <param name="IdControlEncrypt"></param>
        /// <returns></returns>
        Task<ResponseDto<ControlEncryptEntity>> GetControlEncryptByIdAsync(int IdControlEncrypt);
    }
}
