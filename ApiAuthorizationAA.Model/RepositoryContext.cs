
namespace ApiAuthorizationAA.Model
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public partial class ApplicationDbContext : IRepositoryContext
    {
        /// <summary>
        /// Override SaveChanges DbContext
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        /// <summary>
        /// Override SaveChangesAsync DbContext
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
