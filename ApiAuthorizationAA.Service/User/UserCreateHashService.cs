
namespace ApiAuthorizationAA.Service.User
{
    using ApiAuthorizationAA.Common.IPersistence.SecureUser;
    using ApiAuthorizationAA.Common.IService.User;
    using ApiAuthorizationAA.Model.Entities.User;

    public class UserCreateHashService : IUserCreateHashService
    {
        #region Fields
        /// <summary>
        /// Interface variable
        /// </summary>
        private readonly IUserCreateHashPersistence userCreateHashPersistence;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCreateHashPersistence"></param>
        public UserCreateHashService(IUserCreateHashPersistence userCreateHashPersistence)
        {
            this.userCreateHashPersistence = userCreateHashPersistence;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create new record with encrypted password
        /// </summary>
        /// <param name="userEntity"></param>
        public void InsertNewEncryptPassword(UserEntity userEntity)
        {

        }
        #endregion
    }
}
