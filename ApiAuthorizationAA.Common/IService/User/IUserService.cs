
namespace ApiAuthorizationAA.Common.IService.User
{
    using Model.Entities.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<ICollection<UserEntity>> GetUsers();

        Task<bool> CreateUser(UserEntity user);
    }
}
