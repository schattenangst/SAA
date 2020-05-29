
namespace ApiAuthorizationAA.Common.IPersistence.Secure
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using ApiAuthorizationAA.Model.Entities.User;
    using System.Threading.Tasks;

    public interface IHistoricHashPersistence
    {
        /// <summary>
        /// Insert new historic salt hash password
        /// </summary>
        /// <param name="siaraHistoricHash">Object <see cref="UserSecureEntity"/></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> InsertNewHistoricUserPassword(SiaraHistoricHash siaraHistoricHash);
    }
}
