
namespace ApiAuthorizationAA.Common.IPersistence.User
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using System.Threading.Tasks;

    public interface IUserCreateHashPersistence
    {
        /// <summary>
        /// Insert a new encrypt password
        /// </summary>
        /// <param name="siaraWebUserHash">User info</param>
        /// <returns>True if correct inserted</returns>
        Task<ResponseDto<bool>> InsertNewEncryptedUserPassword(SiaraWebUserHash siaraWebUserHash);
    }
}
