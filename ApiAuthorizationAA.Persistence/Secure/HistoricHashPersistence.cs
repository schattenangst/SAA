
namespace ApiAuthorizationAA.Persistence.Secure
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Common.IPersistence.Secure;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using System.Threading.Tasks;

    public class HistoricHashPersistence : BasePersistence<SiaraHistoricHash>, IHistoricHashPersistence
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public HistoricHashPersistence(IRepositoryContext repositoryContext)
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
        public async Task<ResponseDto<bool>> InsertNewHistoricUserPassword(SiaraHistoricHash siaraHistoricHash)
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
    }
}
