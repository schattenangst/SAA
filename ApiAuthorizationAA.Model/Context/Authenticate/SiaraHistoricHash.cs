
namespace ApiAuthorizationAA.Model.Context.Authenticate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(("SiaraHistoricoHash"), Schema = "secure")]
    public class SiaraHistoricHash
    {
        [Key]
        [Column("IdSiaraHistoricoHash")]
        public int IdSiaraHistoricHash { get; set; }

        [Required]
        [Column("id_usuario")]
        public string IdSiaraWebUser { get; set; }

        [Required]
        [Column("IdControlCifrado")]
        public int IdControlEncrypt { get; set; }

        [Required]
        [Column("HistoricoHash")]
        [MaxLength]
        public string HistoricHash { get; set; }

        [Required]
        [Column("FechaRegistro")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [Required]
        [Column("Activo")]
        public bool IsActive { get; set; } = true;

        public virtual SiaraWebUser SiaraWebUser { get; set; }
        public virtual ControlEncrypt ControlEncrypt { get; set; }
    }
}