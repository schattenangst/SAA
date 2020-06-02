
namespace ApiAuthorizationAA.Common.Security
{
    using ApiAuthorizationAA.Common.Resources;
    using Cnbv.Sait.Cryptography;

    /// <summary>
    /// 
    /// </summary>
    public static class ExternalCryptography
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string CipherString(string expression)
        {
            SpecializedSymmetricCompatible method = new SpecializedSymmetricCompatible();


            return method.Cipher(CommonResources.KeyCifrado, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecipherString(string cipherText)
        {
            SpecializedSymmetricCompatible method = new SpecializedSymmetricCompatible();

            return method.Decipher(CommonResources.KeyCifrado, cipherText);
        }
        #endregion
    }
}
