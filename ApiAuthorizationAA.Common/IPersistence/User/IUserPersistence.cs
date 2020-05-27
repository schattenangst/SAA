
namespace ApiAuthorizationAA.Common.IPersistence.User
{
    using Model.Context.Authenticate;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserPersistence : IBasePersistence<SiaraWebUser>
    {
        Task<ICollection<SiaraWebUser>> GetUsers();
    }
}
