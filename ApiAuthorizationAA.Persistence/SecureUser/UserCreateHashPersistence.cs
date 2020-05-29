
namespace ApiAuthorizationAA.Persistence.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Common.IPersistence.SecureUser;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class UserCreateHashPersistence : BasePersistence<SiaraWebUserHash>, IUserCreateHashPersistence
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public UserCreateHashPersistence(IRepositoryContext repositoryContext)
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
            ResponseDto<bool> response = new ResponseDto<bool>(false);

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

        #endregion
    }
}
