
namespace ApiAuthorizationAA.Model.Context.Authenticate
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SiaraUsuarioWebHash", Schema = "secure")]
    public class SiaraWebUserHash
    {
        //[Key]
        //[Column("IdUsuarioWebPassword")]
        //public int IdSiaraWebUserHash { get; set; }

        /// <summary>
        /// ForeignKey with <see cref="SiaraWebUser"/>
        /// </summary>
        [Key]
        [Column("id_usuario")]
        [StringLength(20)]
        public string IdSiaraWebUser { get; set; }

        /// <summary>
        /// Id Control encrypt, foreignkey with <see cref="ControlEncrypt"/>
        /// Configuration by encrypt
        /// </summary>
        [Required]
        [Column("IdControlCifrado")]
        public int IdControlEncrypt { get; set; }

        [Required]
        [MaxLength]
        public string SaltHash { get; set; }

        [Required]
        [MaxLength]
        public string PasswordHash { get; set; }

        [Required]
        [Column("FechaRegistro")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [Column("FechaModificacion")]
        public DateTime? RegisterUpdate { get; set; }

        [Required]
        [Column("Activo")]
        public bool IsActive { get; set; } = false;

        public virtual SiaraWebUser SiaraWebUser { get; set; }
        public virtual ControlEncrypt ControlEncrypt { get; set; }
    }
}