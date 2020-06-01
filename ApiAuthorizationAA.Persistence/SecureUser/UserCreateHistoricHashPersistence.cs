
namespace ApiAuthorizationAA.Persistence.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
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
        /// <param name="siaraHistoricHash">Object <see cref="UserSecureEntity"/></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> InsertNewHistoricUserPasswordAsync(SiaraHistoricHash siaraHistoricHash)
        {
            ResponseDto<bool> response = new ResponseDto<bool>(false);

            try
            {
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
                response = new ResponseDto<bool>($"Error al insertar contraseña historica de usuario {siaraHistoricHash.IdSiaraWebUser}", ex);
            }

            return response;
        }
        #endregion

        #region Private Methods


        #endregion
    }
}
