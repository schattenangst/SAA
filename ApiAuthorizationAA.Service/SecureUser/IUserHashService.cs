
namespace ApiAuthorizationAA.Service.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Entities.User;
    using System.Threading.Tasks;

    public interface IUserHashService
    {

        Task<ResponseDto<bool>> CreateNewUserHashPassword(UserEntity userEntity);

        Task<ResponseDto<bool>> UpdateUserHashPassword(UserEntity userEntity);
    }
}
