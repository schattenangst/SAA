
namespace ApiAuthorizationAA.Common.IPersistence.User
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Entities.User;
    using System.Threading.Tasks;

    public interface IUserCreateHistoricHashPersistence
    {
        /// <summary>
        /// Insert new historic salt hash password
        /// </summary>
        /// <param name="userSecureEntity">Object <see cref="UserSecureEntity"/></param>
        /// <param name="idControlEncrypt">Id control encrypt from <see cref="SiaraHistoricHash"/></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> InsertNewHistoricUserPassword(UserSecureEntity userSecureEntity, int idControlEncrypt);
    }
}
