using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAuthorizationAA.Model.Context.Authenticate
{
    [Table(nameof(UsuarioTest), Schema = "dbo")]
    public class UsuarioTest
    {
        [Key]
        public int IdUsuariosTest { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}
