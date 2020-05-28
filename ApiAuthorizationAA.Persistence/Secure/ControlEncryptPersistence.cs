
namespace ApiAuthorizationAA.Persistence.Secure
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Common.IPersistence.Secure;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class ControlEncryptPersistence : BasePersistence<ControlEncrypt>, IControlEncryptPersistence
    {
        #region Fields
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public ControlEncryptPersistence(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// Insert new configuration to encrypt 
        /// </summary>
        /// <param name="controlEncrypt">Object <see cref="ControlEncrypt"/> with info</param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> InsertNewConfiguration(ControlEncrypt controlEncrypt)
        {
            ResponseDto<bool> response = new ResponseDto<bool>(false);

            try
            {
                ControlEncrypt result = await Create(controlEncrypt);

                // Validate result is null
                if (result != null)
                {
                    response = new ResponseDto<bool>(true);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseDto<bool>("Error al insertar configuración de cifrado.", ex);
            }

            return response;
        }

        /// <summary>
        /// Update configuration only active or inactive
        /// </summary>
        /// <param name="idControlEncrypt">Key configuration</param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> UpdateOffConfigurationAsync(int idControlEncrypt)
        {
            ResponseDto<bool> response = new ResponseDto<bool>(false);

            try
            {
                ControlEncrypt result = await FindFirstAsync(x => x.IdControlEncrypt == idControlEncrypt
                                                             && x.IsActive == true);

                if (result != null)
                {
                    result.IsActive = false;
                    result = await Edit(result);
                }

                // Validate result is null
                if (result != null)
                {
                    response = new ResponseDto<bool>(true);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseDto<bool>("Error al actualizar configuración de cifrado.", ex);
            }

            return response;
        }
        #endregion
    }
}
