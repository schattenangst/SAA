
namespace ApiAuthorizationAA.Persistence.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using ApiAuthorizationAA.Model.Entities.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserHistoricHashPersistence : BasePersistence<SiaraHistoricHash>, IUserHistoricHashPersistence
    {
        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public UserHistoricHashPersistence(IRepositoryContext repositoryContext)
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
                // Disable previous historic records
                bool status = await DisableLastHistoricRecord(siaraHistoricHash.IdSiaraWebUser);

                if (status)
                {
                    // Insert new record
                    SiaraHistoricHash result = await Create(siaraHistoricHash);

                    // Validate result is null
                    if (result != null)
                    {
                        response = new ResponseDto<bool>(true);
                    }
                }
            }
            catch (System.Exception ex)
            {
                response = new ResponseDto<bool>($"Error al insertar contraseña historica de usuario {siaraHistoricHash.IdSiaraWebUser}", ex);
            }

            return response;
        }

        /// <summary>
        /// Get last 5 records by idSiaraWebUser
        /// </summary>
        /// <param name="idSiaraWebUser"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResponseDto<ICollection<SiaraHistoricHash>>> GetUserHistoricHashAsync(string idSiaraWebUser, int pageSize)
        {
            ResponseDto<ICollection<SiaraHistoricHash>> response = null;

            try
            {
                ICollection<SiaraHistoricHash> result = (await FindAllAsync(x => x.IdSiaraWebUser == idSiaraWebUser))
                                                                           .OrderByDescending(x => x.IdSiaraHistoricHash)
                                                                           .Take(pageSize)
                                                                           .ToList();
            }
            catch (System.Exception ex)
            {
                response = new ResponseDto<ICollection<SiaraHistoricHash>>($"Ocurrió un error al obtener historico de contraseñas para el usuario {idSiaraWebUser}", ex);
            }

            return response;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSiaraWebUser"></param>
        /// <returns></returns>
        private async Task<bool> DisableLastHistoricRecord(string idSiaraWebUser)
        {
            bool status = false;

            try
            {
                ICollection<SiaraHistoricHash> results = await FindAllAsync(x => x.IsActive == true);

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
                    status = true;
                }
            }
            catch (System.Exception)
            {
                status = false;
            }

            return status;
        }
        #endregion
    }
}
