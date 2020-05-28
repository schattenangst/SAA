
namespace ApiAuthorizationAA.Persistence.User
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Common.IPersistence.User;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using ApiAuthorizationAA.Model.Entities.User;
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
        public async Task<ResponseDto<bool>> InsertNewEncryptedUserPassword(SiaraWebUserHash siaraWebUserHash)
        {
            ResponseDto<bool> response = new ResponseDto<bool>(false);

            try
            {
                //// Instance new object SiaraWebHash
                //SiaraWebUserHash siaraWebUserHash = new SiaraWebUserHash
                //{
                //    IdSiaraWebUser = userSecureEntity.IdUserWeb,
                //    SaltHash = userSecureEntity.Salt,
                //    PasswordHash = userSecureEntity.PasswordHash,
                //    IsActive = true,
                //};

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
                response = new ResponseDto<bool>($"Error al insertar constraseña de usuario {siaraWebUserHash.IdSiaraWebUser}", ex);
            }

            return response;
        }

        #endregion
    }
}
