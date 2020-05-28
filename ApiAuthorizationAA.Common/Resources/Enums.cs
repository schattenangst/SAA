namespace ApiAuthorizationAA.Common.Resources
{
    /// <summary>
    /// Error codes by application
    /// </summary>
    public class Enums
    {       /// <summary>
            /// Enum con codigos de error
            /// </summary>
        public enum Error
        {
            /// <summary>
            /// Not error
            /// </summary>
            Ok = 0,

            /// <summary>
            /// Error code unknow or not controlled
            /// </summary>
            UnknowError = 100
        }
    }
}
