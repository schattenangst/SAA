
namespace ApiAuthorizationAA.Common.IPersistence.Secure
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
        Task<ResponseDto<bool>> InsertNewConfigurationAsync(ControlEncrypt controlEncrypt);

        /// <summary>
        /// Update configuration only active or inactive
        /// </summary>
        /// <param name="idControlEncrypt">Key configuration</param>
        /// <returns></returns>
        Task<ResponseDto<bool>> UpdateOffConfigurationAsync(int idControlEncrypt);
    }
}
