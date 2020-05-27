
namespace ApiAuthotizationAA.Model.Entities.Cryptography
{
    /// <summary>
    /// Container for password hash and salt and iterations.
    /// </summary>
    public sealed class PasswordHashContainerEntity
    {
        /// <summary>
        /// Gets the hashed password.
        /// </summary>
        public byte[] HashedPassword { get; private set; }

        /// <summary>
        /// Gets the salt.
        /// </summary>
        public byte[] Salt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordHashContainerEntity" /> class.
        /// </summary>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="salt">The salt.</param>
        public PasswordHashContainerEntity(byte[] hashedPassword, byte[] salt)
        {
            this.HashedPassword = hashedPassword;
            this.Salt = salt;
        }
    }
}