
namespace ApiAuthorizationAA.Service.User
{
    using ApiAuthorizationAA.Model.Entities.User;
    using Common.IPersistence.User;
    using Common.IService.User;
    using Model.Context.Authenticate;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IUserPersistence userPersistence;

        public UserService(IUserPersistence userPersistence)
        {
            this.userPersistence = userPersistence;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<bool> CreateUser(UserEntity newUser)
        {
            try
            {
                SiaraWebUser newSiaraWebUser = new SiaraWebUser
                {
                    IdSiaraWebUser = newUser.UserName,
                    Active = true,
                };

                var result = await userPersistence.Create(newSiaraWebUser);

                return result != null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<UserEntity>> GetUsers()
        {
            List<SiaraWebUser> result = (await userPersistence.GetUsers()).ToList();

            List<UserEntity> users = new List<UserEntity>();

            foreach (SiaraWebUser user in result)
            {
                UserEntity newUser = new UserEntity
                {
                    UserName = user.IdSiaraWebUser,
                    Password = user.Active.ToString()
                };

                users.Add(newUser);
            }

            return users;
        }
    }
}
