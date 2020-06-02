
namespace ApiAuthorizationAA.Persistence.EncryptConfiguration
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using ApiAuthorizationAA.Model.Entities.EncryptConfiguration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        #endregion

        #region Public Methods
        /// <summary>
        /// Insert new configuration to encrypt 
        /// </summary>
        /// <param name="controlEncrypt">Object <see cref="ControlEncrypt"/> with info</param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> CreateConfigurationAsync(ControlEncrypt controlEncrypt)
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
        /// <returns></returns>
        public async Task<ResponseDto<bool>> DisableConfigurationAllAsync()
        {
            ResponseDto<bool> response = new ResponseDto<bool>(false);

            try
            {
                ICollection<ControlEncrypt> results = await FindAllAsync(x => x.IsActive == true);

                if (results != null && results.Count() > 0)
                {
                    foreach (var item in results)
                    {
                        if (item.IsActive)
                        {
                            item.IsActive = false;
                            var temp = await Edit(item);
                        }
                    }
                }

                // Validate result is null
                if (results != null)
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

        /// <summary>
        /// Update configuration only active or inactive
        /// </summary>
        /// <param name="idControlEncrypt">Key configuration</param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> DisableConfigurationByIdAsync(int idControlEncrypt)
        {
            ResponseDto<bool> response = new ResponseDto<bool>(false);

            try
            {
                ControlEncrypt result = await FindFirstAsync(x => x.IdControlEncrypt == idControlEncrypt
                                                                           && x.IsActive == true);

                // Validate result is null
                if (result != null)
                {
                    result.IsActive = false;
                    var temp = await Edit(result);
                    response = new ResponseDto<bool>(true);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseDto<bool>("Error al actualizar configuración de cifrado.", ex);
            }

            return response;
        }

        /// <summary>
        /// Get last record active from encrypt configuration
        /// </summary>
        /// <returns>Objecto with data <see cref="ControlEncryptEntity"/></returns>
        public async Task<ResponseDto<ControlEncryptEntity>> GetCurrentControlEncryptAsync()
        {
            ResponseDto<ControlEncryptEntity> response = null;

            try
            {
                // Get last active record 
                ControlEncrypt result = (await FindAllAsync(x => x.IsActive == true)).OrderByDescending(x => x.RegisterDate).FirstOrDefault();

                if (result != null)
                {
                    // ToDo: Change to mapper
                    ControlEncryptEntity controlEncryptEntity = new ControlEncryptEntity
                    {
                        IdControlEncrypt = result.IdControlEncrypt,
                        HashSize = result.HashSize,
                        SaltSize = result.SaltSize,
                        Iterations = result.Iterations,
                    };

                    response = new ResponseDto<ControlEncryptEntity>(controlEncryptEntity);
                }

            }
            catch (Exception ex)
            {
                response = new ResponseDto<ControlEncryptEntity>("Error al obtener configuración actual de cifrado.", ex);
            }

            return response;
        }

        /// <summary>
        /// Get configuration encrypt by Id
        /// </summary>
        /// <param name="IdControlEncrypt"></param>
        /// <returns></returns>
        public async Task<ResponseDto<ControlEncrypt>> GetControlEncryptByIdAsync(int IdControlEncrypt)
        {
            ResponseDto<ControlEncrypt> response = null;

            try
            {
                // Get configuration by id
                ControlEncrypt result = await FindFirstAsync(x => x.IdControlEncrypt == IdControlEncrypt);

                response = new ResponseDto<ControlEncrypt>(result);
            }
            catch (Exception ex)
            {
                response = new ResponseDto<ControlEncrypt>("Error al obtener configuración de cifrado.", ex);
            }

            return response;
        }
        #endregion

        #region Private Methods
        private bool InactiveControl(bool isActive)
        {
            bool temp = false;

            if (isActive)
            {
                temp = false;
            }

            return temp;
        }
        #endregion
    }
}
