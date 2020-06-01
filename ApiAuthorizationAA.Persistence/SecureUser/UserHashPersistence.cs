
namespace ApiAuthorizationAA.Persistence.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class UserHashPersistence : BasePersistence<SiaraWebUserHash>, IUserHashPersistence
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public UserHashPersistence(IRepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Insert a new encrypt password
        /// </summary>
        /// <param name="siaraWebUserHash">User info</param>
        /// <returns>True if correct inserted</returns>
        public async Task<ResponseDto<bool>> InsertNewEncryptedUserPasswordAsync(SiaraWebUserHash siaraWebUserHash)
        {
            ResponseDto<bool> response = null;

            try
            {
                // Inserte new record
                SiaraWebUserHash result = await Create(siaraWebUserHash);

                // Validate result is null
                if (result != null)
                {
                    response = new ResponseDto<bool>(true);
                }
            }
            catch (System.Exception ex)
            {
                response = new ResponseDto<bool>($"Error al insertar constraseña cifrada de usuario {siaraWebUserHash.IdSiaraWebUser}", ex);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siaraWebUserHash"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> UpdateUserHashPasswordAsync(SiaraWebUserHash siaraWebUserHash)
        {
            ResponseDto<bool> response = null;

            try
            {
                // Get siara web user hash
                SiaraWebUserHash webUserHash = await FindFirstAsync(x => x.IdSiaraWebUser == siaraWebUserHash.IdSiaraWebUser);

                if (webUserHash != null)
                {
                    webUserHash.IdControlEncrypt = siaraWebUserHash.IdControlEncrypt;
                    webUserHash.PasswordHash = siaraWebUserHash.PasswordHash;
                    webUserHash.SaltHash = siaraWebUserHash.SaltHash;
                    webUserHash.RegisterUpdate = DateTime.Now;

                    SiaraWebUserHash result = await Edit(webUserHash);

                    if (result != null)
                    {
                        response = new ResponseDto<bool>(true);
                    }
                }
            }
            catch (System.Exception ex)
            {
                response = new ResponseDto<bool>($"Error al actualizar la constraseña cifrada de usuario {siaraWebUserHash.IdSiaraWebUser}", ex);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> ValidateExistUserHash(string idUser)
        {
            ResponseDto<bool> response = null;

            try
            {
                int countRegister = await CountAsync(x => x.IdSiaraWebUser == idUser);

                response = new ResponseDto<bool>(countRegister > 0 ? true : false);
            }
            catch (System.Exception ex)
            {
                response = new ResponseDto<bool>("Ocurrió un error al validar el usuario.", ex);

            }

            return response;
        }

        #endregion
    }
}
