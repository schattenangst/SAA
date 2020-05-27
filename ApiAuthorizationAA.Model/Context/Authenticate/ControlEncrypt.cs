
namespace ApiAuthorizationAA.Model.Context.Authenticate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ControlCifrado", Schema = "secure")]
    public class ControlEncrypt
    {
        [Key]
        [Column("IdControlCifrado")]
        public int IdControlEncrypt { get; set; }

        [Required]
        public byte SaltSize { get; set; }

        [Required]
        public byte HashSize { get; set; }

        [Required]
        [Column("Iteraciones")]
        public short Iterations { get; set; }

        [Required]
        [Column("FechaRegistro")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [Required]
        [Column("Activo")]
        public bool IsActive { get; set; }

        public virtual ICollection<SiaraHistoricHash> SiaraHistoricHashes { get; set; }
    }
}