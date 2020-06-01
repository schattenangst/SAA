
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
    public class UserCreateHashService : IUserCreateHashService
    {
        #region Fields
        private readonly IControlEncryptPersistence controlEncryptPersistence;
        private readonly IUserCreateHashPersistence userCreateHashPersistence;
        private readonly IUserCreateHistoricHashPersistence userCreateHistoricHashPersistence;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCreateHashPersistence"></param>
        /// <param name="userCreateHistoricHashPersistence"></param>
        /// <param name="controlEncryptPersistence"></param>
        public UserCreateHashService(IUserCreateHashPersistence userCreateHashPersistence,
                                     IUserCreateHistoricHashPersistence userCreateHistoricHashPersistence,
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
                // Get last configuration encrypt
                ControlEncryptEntity controlEncryptEntity = (await GetConfigurationEncrypt()).Response;

                // Password hash and salt generated
                PasswordHashContainerEntity hashResult = EncryptHashProvider.CreateHash(userEntity.Password, controlEncryptEntity);

                // Save historic hash by user
                SiaraHistoricHash siaraHistoricHash = new SiaraHistoricHash
                {
                    IdSiaraWebUser = userEntity.IdUserWeb,
                    IdControlEncrypt = controlEncryptEntity.IdControlEncrypt,
                    HistoricSaltHash = Convert.ToBase64String(hashResult.Salt),
                };

                //userCreateHashPersistence.InsertNewEncryptedUserPasswordAsync(siara)

                var resultHistoric = await userCreateHistoricHashPersistence.InsertNewHistoricUserPasswordAsync(siaraHistoricHash);

                response = new ResponseDto<bool>(true);
            }
            catch (Exception ex)
            {
                response = new ResponseDto<bool>($"Ocurrio un error al obtener Hash.", ex);
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
        #endregion

    }
}
