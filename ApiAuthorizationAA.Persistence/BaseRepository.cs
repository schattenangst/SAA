
namespace ApiAuthorizationAA.Persistence
{
    using Model;
    using System.Data.Entity;

    public class BaseRepository
    {
        private readonly IRepositoryContext repositoryContext;

        public BaseRepository(IRepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public DbContext Context
        {
            get
            {
                return repositoryContext as ApplicationDbContext;
            }
        }
    }
}
