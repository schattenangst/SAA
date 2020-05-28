
namespace ApiAuthorizationAA.Model.Context.Authenticate
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(("SiaraHistoricoHash"), Schema = "secure")]
    public class SiaraHistoricHash
    {
        /// <summary>
        /// Id increment
        /// </summary>
        [Key]
        [Column("IdSiaraHistoricoHash")]
        public int IdSiaraHistoricHash { get; set; }

        /// <summary>
        /// Id usuario Siara_cat_usuarios_web
        /// </summary>
        [Required]
        [Column("id_usuario")]
        public string IdSiaraWebUser { get; set; }

        /// <summary>
        /// Id Control encrypt 
        /// Configuration by encrypt
        /// </summary>
        [Required]
        [Column("IdControlCifrado")]
        public int IdControlEncrypt { get; set; }

        /// <summary>
        /// Save salt with which the password was encrypted
        /// </summary>
        [Required]
        [Column("HistoricoSaltHash")]
        [MaxLength]
        public string HistoricSaltHash { get; set; }

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