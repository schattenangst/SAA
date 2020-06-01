
namespace ApiAuthorizationAA.Model
{
    using Context.Authenticate;
    using System.Data.Common;
    using System.Data.Entity;

    using System.Data.Entity.ModelConfiguration.Conventions;

    /// <summary>
    /// 
    /// </summary>
    public partial class ApplicationDbContext : DbContext
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public ApplicationDbContext()
            : base("name=Model1")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public ApplicationDbContext(string connection)
            : base(connection)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="contextConnection"></param>
        public ApplicationDbContext(DbConnection dbConnection, bool contextConnection)
         : base(dbConnection, contextConnection)
        {
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SiaraWebUser>();
            modelBuilder.Entity<ControlEncrypt>();

            // Historico de contraseñas por usuario
            modelBuilder.Entity<SiaraHistoricHash>()
                .HasRequired(s => s.SiaraWebUser)
                .WithMany(g => g.SiaraHistoricHashes)
                .HasForeignKey(s => s.IdSiaraWebUser);

            modelBuilder.Entity<SiaraHistoricHash>()
                .HasRequired(s => s.ControlEncrypt)
                .WithMany(g => g.SiaraHistoricHashes)
                .HasForeignKey(s => s.IdControlEncrypt);

            // Contraseña actual cifrada por usuario
            modelBuilder.Entity<SiaraWebUserHash>()
                .HasRequired(s => s.SiaraWebUser)
                .WithRequiredDependent(g => g.SiaraWebUserHash);

            modelBuilder.Entity<SiaraWebUserHash>()
                .HasRequired(s => s.ControlEncrypt)
                .WithMany(g => g.SiaraWebUserHashes)
                .HasForeignKey(s => s.IdControlEncrypt);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // Secure user password
        public DbSet<ControlEncrypt> ControlEncrypt { get; set; }
        public DbSet<SiaraHistoricHash> SiaraHistoricHashes { get; set; }
        public DbSet<SiaraWebUser> SiaraWebUsers { get; set; }
        public DbSet<SiaraWebUserHash> SiaraWebUserHashes { get; set; }
    }
}
