
namespace ApiAuthorizationAA.Common.Security
{
    using ApiAuthorizationAA.Model.Entities.EncryptConfiguration;
    using ApiAuthotizationAA.Model.Entities.Cryptography;
    using System;
    using System.Linq;
    using System.Security.Cryptography;

    /// <summary>
    /// Salted password hashing with PBKDF2-SHA1.
    /// Compatibility: .NET 3.0 and later.
    /// </summary>
    /// <remarks>See http://crackstation.net/hashing-security.htm for much more on password hashing.</remarks>
    public static class EncryptHashProvider
    {
        #region Fields
        #endregion

        #region Public Methods
        #endregion

        #region Properties
        #endregion

        /// <summary>
        /// The salt byte size, 64 length ensures safety but could be increased / decreased
        /// </summary>
        //private const int saltByteSize = 64;
        /// <summary>
        /// The hash byte size, 
        /// </summary>
        //private const int hashByteSize = 64;
        /// <summary>
        ///  High iteration count is less likely to be cracked
        /// </summary>
        //private const int pbkdf2Iterations = 5000; // Min 5000 / Max 10000 less performance

        /// <summary>
        /// Creates a salted PBKDF2 hash of the password.
        /// </summary>
        /// <remarks>
        /// The salt and the hash have to be persisted side by side for the password. 
        /// They could be persisted as bytes or as a string using the convenience methods 
        /// in the next class to convert from byte[] to string and later back again when executing password validation.
        /// </remarks>
        /// <param name="password">The password to hash.</param>
        /// <param name="controlEncryptEntity">Configuration to encrypt</param>
        /// <returns>The hash of the password.</returns>
        public static PasswordHashContainerEntity CreateHash(string password, ControlEncryptEntity controlEncryptEntity)
        {
            // Generate a random salt
            using (var csprng = new RNGCryptoServiceProvider())
            {
                // create a unique salt for every password hash to prevent rainbow and dictionary based attacks
                byte[] salt = new byte[controlEncryptEntity.SaltSize];
                csprng.GetBytes(salt);

                // Hash the password and encode the parameters
                byte[] hash = Pbkdf2(password, salt, controlEncryptEntity.Iterations, controlEncryptEntity.HashSize);

                return new PasswordHashContainerEntity(hash, salt);
            }
        }

        ///// <summary>
        ///// Creates a salted PBKDF2 hash of the password.
        ///// </summary>
        ///// <remarks>
        ///// The salt and the hash have to be persisted side by side for the password. 
        ///// They could be persisted as bytes or as a string using the convenience methods 
        ///// in the next class to convert from byte[] to string and later back again when executing password validation.
        ///// </remarks>
        ///// <param name="password">The password to hash.</param>
        ///// <returns>The hash of the password.</returns>
        //public static PasswordHashContainerEntity CreateHash(string password)
        //{
        //    // Generate a random salt
        //    using (var csprng = new RNGCryptoServiceProvider())
        //    {
        //        // create a unique salt for every password hash to prevent rainbow and dictionary based attacks
        //        byte[] salt = new byte[saltByteSize];
        //        csprng.GetBytes(salt);

        //        // Hash the password and encode the parameters
        //        byte[] hash = Pbkdf2(password, salt, pbkdf2Iterations, hashByteSize);

        //        return new PasswordHashContainerEntity(hash, salt);
        //    }
        //}

        /// <summary>
        /// Recreates a password hash based on the incoming password string and the stored salt
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="salt">The salt existing.</param>
        /// <returns>the generated hash based on the password and salt</returns>
        public static byte[] CreateHash(string password, byte[] salt, ControlEncryptEntity controlEncryptEntity)
        {
            // Extract the parameters from the hash
            return Pbkdf2(password, salt, controlEncryptEntity.Iterations, controlEncryptEntity.HashSize);
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="salt">The existing stored salt.</param>
        /// <param name="correctHash">The hash of the existing password.</param>
        /// <returns><c>true</c> if the password is correct. <c>false</c> otherwise. </returns>
        public static bool ValidatePassword(string password, byte[] salt, byte[] correctHash, ControlEncryptEntity controlEncryptEntity)
        {
            // Extract the parameters from the hash
            byte[] testHash = Pbkdf2(password, salt, controlEncryptEntity.Iterations, controlEncryptEntity.HashSize);
            return CompareHashes(correctHash, testHash);
        }

        /// <summary>
        /// Compares two byte arrays (hashes)
        /// </summary>
        /// <param name="array1">The array1.</param>
        /// <param name="array2">The array2.</param>
        /// <returns><c>true</c> if they are the same, otherwise <c>false</c></returns>
        public static bool CompareHashes(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length) return false;
            return !array1.Where((t, i) => t != array2[i]).Any();
        }

        /// <summary>
        /// Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {

            //using (SHA512CryptoServiceProvider providerSha512 = new SHA512CryptoServiceProvider())
            //{
            //    byte[] output = providerSha512.ComputeHash(Encoding.ASCII.GetBytes(password));
            //}

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }


    /// <summary>
    /// Convenience methods for converting between hex strings and byte array.
    /// </summary>
    public static class ByteConverter
    {
        /// <summary>
        /// Converts the hex representation string to an array of bytes
        /// </summary>
        /// <param name="hexedString">The hexed string.</param>
        /// <returns></returns>
        public static byte[] GetHexBytes(string hexedString)
        {
            var bytes = new byte[hexedString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var strPos = i * 2;
                var chars = hexedString.Substring(strPos, 2);
                bytes[i] = Convert.ToByte(chars, 16);
            }
            return bytes;
        }

        /// <summary>
        /// Gets a hex string representation of the byte array passed in.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public static string GetHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
        }
    }
}