
namespace ApiAuthorizationAA.Persistence.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using System.Threading.Tasks;

    public interface IUserHashPersistence
    {
        /// <summary>
        /// Insert a new encrypt password
        /// </summary>
        /// <param name="siaraWebUserHash">User info</param>
        /// <returns>True if correct inserted</returns>
        Task<ResponseDto<bool>> InsertNewEncryptedUserPasswordAsync(SiaraWebUserHash siaraWebUserHash);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siaraWebUserHash"></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> UpdateUserHashPasswordAsync(SiaraWebUserHash siaraWebUserHash);

        Task<ResponseDto<bool>> ValidateExistUserHash(string idUser);


    }
}
