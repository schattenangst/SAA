
namespace ApiAuthorizationAA.Service.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Common.Security;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using ApiAuthorizationAA.Model.Entities.EncryptConfiguration;
    using ApiAuthorizationAA.Model.Entities.User;
    using ApiAuthorizationAA.Persistence.EncryptConfiguration;
    using ApiAuthorizationAA.Persistence.SecureUser;
    using ApiAuthotizationAA.Model.Entities.Cryptography;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class UserHashService : IUserHashService
    {
        #region Fields
        private readonly IControlEncryptPersistence controlEncryptPersistence;
        private readonly IUserHashPersistence userCreateHashPersistence;
        private readonly IUserHistoricHashPersistence userCreateHistoricHashPersistence;

        private SiaraHistoricHash siaraHistoricHash = null;
        private SiaraWebUserHash siaraWebUserHash = null;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCreateHashPersistence"></param>
        /// <param name="userCreateHistoricHashPersistence"></param>
        /// <param name="controlEncryptPersistence"></param>
        public UserHashService(IUserHashPersistence userCreateHashPersistence,
                                     IUserHistoricHashPersistence userCreateHistoricHashPersistence,
                                     IControlEncryptPersistence controlEncryptPersistence)
        {
            this.userCreateHashPersistence = userCreateHashPersistence;
            this.userCreateHistoricHashPersistence = userCreateHistoricHashPersistence;
            this.controlEncryptPersistence = controlEncryptPersistence;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> CreateNewUserHashPassword(UserEntity userEntity)
        {
            ResponseDto<bool> response = null;

            try
            {
                // *****************************************
                // Valida si ya existe registro del usuario
                ResponseDto<bool> result = await userCreateHashPersistence.ValidateExistUserHash(userEntity.UserName);

                if (result != null && !result.Response)
                {
                    // Get last configuration encrypt
                    ControlEncryptEntity controlEncryptEntity = (await GetConfigurationEncrypt()).Response;

                    // Password hash and salt generated
                    InitializeRecordInfo(userEntity, controlEncryptEntity);

                    // Insert new record at SiaraWebUserHash
                    response = await userCreateHashPersistence.InsertNewEncryptedUserPasswordAsync(siaraWebUserHash);

                    // Validate operation
                    if (response != null && !response.IsError)
                    {
                        // Insert new record at SiaraHistoricHash
                        response = await userCreateHistoricHashPersistence.InsertNewHistoricUserPasswordAsync(siaraHistoricHash);

                        // validate operation
                        if (response != null && !response.IsError)
                        {
                            // instance true response
                            response = new ResponseDto<bool>(true);
                        }
                    }
                }
                else
                {
                    response = new ResponseDto<bool>($"El usuario {userEntity.UserName} ya cuenta con registro.");
                }
            }
            catch (Exception ex)
            {
                response = new ResponseDto<bool>($"Ocurrio un error al obtener Hash.", ex);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> UpdateUserHashPassword(UserEntity userEntity)
        {
            ResponseDto<bool> response = null;

            try
            {
                // ***********************************************
                ResponseDto<bool> result = await userCreateHashPersistence.ValidateExistUserHash(userEntity.UserName);

                if (result != null && result.Response)
                {
                    // Get last configuration encrypt
                    ControlEncryptEntity controlEncryptEntity = (await GetConfigurationEncrypt()).Response;

                    InitializeRecordInfo(userEntity, controlEncryptEntity);

                    // Update record SiaraWebUser
                    response = await userCreateHashPersistence.UpdateUserHashPasswordAsync(siaraWebUserHash);

                    // Validate operation
                    if (response != null && !response.IsError)
                    {
                        // Insert new record at SiaraHistoricHash
                        response = await userCreateHistoricHashPersistence.InsertNewHistoricUserPasswordAsync(siaraHistoricHash);

                        // validate operation
                        if (response != null && !response.IsError)
                        {
                            // instance true response
                            response = new ResponseDto<bool>(true);
                        }
                    }
                }
                else
                {
                    response = new ResponseDto<bool>($"El usuario {userEntity.UserName} no cuenta con registro para actualizar su información.");
                }
            }
            catch (Exception ex)
            {
                response = new ResponseDto<bool>($"Ocurrio un error al actualizar la contraseña del usuario {userEntity.UserName}.", ex);
            }

            return response;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseDto<ControlEncryptEntity>> GetConfigurationEncrypt()
        {
            ResponseDto<ControlEncryptEntity> response = null;

            try
            {
                response = await controlEncryptPersistence.GetCurrentControlEncryptAsync();
            }
            catch (Exception ex)
            {
                response = new ResponseDto<ControlEncryptEntity>($"Ocurrio un error al obtener Hash.", ex);

            }

            return response;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="controlEncryptEntity"></param>
        private void InitializeRecordInfo(UserEntity userEntity, ControlEncryptEntity controlEncryptEntity)
        {
            // Password hash and salt generated
            PasswordHashContainerEntity hashResult = EncryptHashProvider.CreateHash(userEntity.Password, controlEncryptEntity);

            // Save historic hash by user
            siaraHistoricHash = new SiaraHistoricHash
            {
                IdSiaraWebUser = userEntity.UserName,
                IdControlEncrypt = controlEncryptEntity.IdControlEncrypt,
                HistoricSaltHash = Convert.ToBase64String(hashResult.Salt),
            };

            siaraWebUserHash = new SiaraWebUserHash
            {
                IdSiaraWebUser = userEntity.UserName,
                IdControlEncrypt = controlEncryptEntity.IdControlEncrypt,
                PasswordHash = Convert.ToBase64String(hashResult.HashedPassword),
                SaltHash = Convert.ToBase64String(hashResult.Salt),
                IsActive = true,
            };
        }
        #endregion
    }
}
