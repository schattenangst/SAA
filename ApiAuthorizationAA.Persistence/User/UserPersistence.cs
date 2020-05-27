
namespace ApiAuthorizationAA.Persistence.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model.Entities.User;
    using Common.IPersistence.User;
    using System.Collections;
    using Model;
    using Model.Context.Authenticate;

    public class UserPersistence : BasePersistence<SiaraWebUser>, IUserPersistence
    {
        public UserPersistence(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<ICollection<SiaraWebUser>> GetUsers()
        {
            try
            {
                ICollection<SiaraWebUser> siaraUsers = await FindAllAsync(x => x.Active == true);
                return siaraUsers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
