
namespace ApiAuthorizationAA.Persistence.User
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Common.IPersistence.User;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using ApiAuthorizationAA.Model.Entities.User;
    using System.Threading.Tasks;

    public class UserCreateHistoricHashPersistence : BasePersistence<SiaraHistoricHash>, IUserCreateHistoricHashPersistence
    {
        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public UserCreateHistoricHashPersistence(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Insert new historic salt hash password
        /// </summary>
        /// <param name="userSecureEntity">Object <see cref="UserSecureEntity"/></param>
        /// <param name="idControlEncrypt">Id control encrypt from <see cref="SiaraHistoricHash"/></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> InsertNewHistoricUserPassword(UserSecureEntity userSecureEntity, int idControlEncrypt)
        {
            ResponseDto<bool> response = new ResponseDto<bool>(false);

            try
            {
                SiaraHistoricHash siaraHistoricHash = new SiaraHistoricHash
                {
                    IdSiaraWebUser = userSecureEntity.IdUserWeb,
                    IdControlEncrypt = idControlEncrypt,
                    HistoricSaltHash = userSecureEntity.Salt,
                };

                // Insert new record
                SiaraHistoricHash result = await Create(siaraHistoricHash);

                // Validate result is null
                if (result != null)
                {
                    response = new ResponseDto<bool>(true);
                }
            }
            catch (System.Exception ex)
            {
                response = new ResponseDto<bool>($"Error al insertar contraseña historica de usuario {userSecureEntity.IdUserWeb}", ex);
            }

            return response;
        }
        #endregion

        #region Private Methods

        
        #endregion
    }
}
