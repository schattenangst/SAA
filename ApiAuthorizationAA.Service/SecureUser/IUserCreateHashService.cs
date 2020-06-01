
namespace ApiAuthorizationAA.Service.SecureUser
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Model.Entities.User;
    using System.Threading.Tasks;

    public interface IUserCreateHashService
    {

        Task<ResponseDto<bool>> CreateNewUserHashPassword(UserEntity userEntity);
    }
}
