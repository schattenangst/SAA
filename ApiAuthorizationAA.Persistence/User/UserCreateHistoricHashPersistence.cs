
namespace ApiAuthorizationAA.Persistence.User
{
    using ApiAuthorizationAA.Common.Dto;
    using ApiAuthorizationAA.Common.IPersistence.User;
    using ApiAuthorizationAA.Model;
    using ApiAuthorizationAA.Model.Context.Authenticate;
    using ApiAuthorizationAA.Model.Entities.User;
    using System.Threading.Tasks;

    public class UserCreateHistoricHashPersistence : BasePersistence<SiaraHistoricHash>, IUserCreateHistoricHashPersistence
    {
        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        public UserCreateHistoricHashPersistence(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods


        #endregion
    }
}
