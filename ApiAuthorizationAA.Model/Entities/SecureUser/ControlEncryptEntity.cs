
namespace ApiAuthorizationAA.Model.Entities.SecureUser
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class ControlEncryptEntity
    {
        public int IdControlEncrypt { get; set; }

        public byte SaltSize { get; set; }

        public byte HashSize { get; set; }

        public short Iterations { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool IsActive { get; set; }
    }
}