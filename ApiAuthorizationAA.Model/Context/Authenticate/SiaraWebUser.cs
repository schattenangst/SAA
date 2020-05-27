
namespace ApiAuthorizationAA.Model.Context.Authenticate
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(("siara_cat_usuarios_web"))]
    public class SiaraWebUser
    {
        [Key]
        [Column("id_usuario")]
        public string IdSiaraWebUser { get; set; }

        [Column("activo")]
        public bool Active { get; set; }

        public virtual ICollection<SiaraHistoricHash> SiaraHistoricHashes { get; set; }

        public virtual ICollection<SiaraWebUserHash> SiaraWebUserHashes { get; set; }
    }
}