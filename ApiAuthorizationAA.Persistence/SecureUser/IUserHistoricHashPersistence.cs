
namespace ApiAuthorizationAA.Persistence.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserHistoricHashPersistence
    {
        /// <summary>
        /// Insert new historic salt hash password
        /// </summary>
        /// <param name="siaraHistoricHash">Object <see cref="UserSecureEntity"/></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> InsertNewHistoricUserPasswordAsync(SiaraHistoricHash siaraHistoricHash);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSiaraWebUser"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ResponseDto<ICollection<SiaraHistoricHash>>> GetUserHistoricHashAsync(string idSiaraWebUser, int pageSize)
    }
}
