
namespace ApiAuthorizationAA.Service.EncryptConfiguration
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using ApiAuthorizationAA.Model.Entities.EncryptConfiguration;
    using ApiAuthorizationAA.Persistence.EncryptConfiguration;
    using System;
    using System.Threading.Tasks;

    public class ControlEncryptService : IControlEncryptPersistence
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private readonly Persistence.EncryptConfiguration.IControlEncryptPersistence controlEncryptPersistence;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor by <see cref="ControlEncryptService"/>
        /// </summary>
        /// <param name="controlEncryptPersistence"></param>
        public ControlEncryptService(Persistence.EncryptConfiguration.IControlEncryptPersistence controlEncryptPersistence)
        {
            this.controlEncryptPersistence = controlEncryptPersistence;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Insert new configuration to encrypt 
        /// </summary>
        /// <param name="controlEncryptEntity">Object <see cref="ControlEncrypt"/> with info</param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> CreateConfigurationAsync(ControlEncryptEntity controlEncryptEntity)
        {
            ResponseDto<bool> response = new ResponseDto<bool>(false);

            // Get active records
            ResponseDto<bool> result = await controlEncryptPersistence.DisableConfigurationAllAsync();

            if (result != null && !result.IsError)
            {
                ControlEncrypt controlEncrypt = MapperEntity(controlEncryptEntity);
                response = await controlEncryptPersistence.CreateConfigurationAsync(controlEncrypt);

                response = new ResponseDto<bool>(response.Response);
            }
            else
            {
                response.IsError = result.IsError;
                response.ErrorMessage = result.ErrorMessage;
            }

            return response;
        }

        /// <summary>
        /// Update configuration only active or inactive
        /// </summary>
        /// <param name="idControlEncrypt">Key configuration</param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> DisableConfigurationByIdAsync(int idControlEncrypt)
        {
            ResponseDto<bool> response = await controlEncryptPersistence.DisableConfigurationByIdAsync(idControlEncrypt);

            return response;
        }

        /// <summary>
        /// Get last active configuration encrypt
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDto<ControlEncryptEntity>> GetCurrentControlEncryptAsync()
        {
            ResponseDto<ControlEncryptEntity> response = await controlEncryptPersistence.GetCurrentControlEncryptAsync();

            // Validate result
            if (response != null && !response.IsError)
            {
                //ControlEncryptEntity controlEncryptEntity =await  MapperEntity(result.Response);
                //response = new ResponseDto<ControlEncryptEntity>();
            }

            return response;
        }

        /// <summary>
        /// Get configuration encrypt by Id
        /// </summary>
        /// <param name="IdControlEncrypt"></param>
        /// <returns></returns>
        public async Task<ResponseDto<ControlEncryptEntity>> GetControlEncryptByIdAsync(int IdControlEncrypt)
        {
            ResponseDto<ControlEncryptEntity> response = null;

            ResponseDto<ControlEncrypt> result = await controlEncryptPersistence.GetControlEncryptByIdAsync(IdControlEncrypt);

            // Validate result
            if (result != null && !result.IsError)
            {
                ControlEncryptEntity controlEncryptEntity = MapperEntity(result.Response);
                response = new ResponseDto<ControlEncryptEntity>(controlEncryptEntity);
            }

            return response;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlEncryptEntity"></param>
        /// <returns></returns>
        private ControlEncrypt MapperEntity(ControlEncryptEntity controlEncryptEntity)
        {
            ControlEncrypt controlEncrypt = new ControlEncrypt
            {
                IdControlEncrypt = controlEncryptEntity.IdControlEncrypt,
                HashSize = controlEncryptEntity.HashSize,
                Iterations = controlEncryptEntity.Iterations,
                SaltSize = controlEncryptEntity.SaltSize,
                IsActive = true,
                RegisterDate = DateTime.Now
            };

            return controlEncrypt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlEncryptEntity"></param>
        /// <returns></returns>
        private ControlEncryptEntity MapperEntity(ControlEncrypt controlEncrypt)
        {
            ControlEncryptEntity controlEncryptEntity = new ControlEncryptEntity
            {
                IdControlEncrypt = controlEncrypt.IdControlEncrypt,
                HashSize = controlEncrypt.HashSize,
                Iterations = controlEncrypt.Iterations,
                SaltSize = controlEncrypt.SaltSize,
                IsActive = controlEncrypt.IsActive,
                RegisterDate = controlEncrypt.RegisterDate
            };

            return controlEncryptEntity;
        }

        #endregion
    }
}
