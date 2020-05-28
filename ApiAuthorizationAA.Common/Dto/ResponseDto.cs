
namespace ApiAuthorizationAA.Common.Dto
{
    using ApiAuthorizationAA.Common.Resources;
    using System;

    /// <summary>
    /// Class for response on solution
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseDto<T>
    {
        #region Constructors
        /// <summary>
        /// Create a instance at correct response
        /// </summary>
        /// <param name="response"></param>
        public ResponseDto(T response)
        {
            Response = response;
        }

        /// <summary>
        /// Create a instance at wrong response
        /// </summary>
        /// <param name="error"></param>
        public ResponseDto(string message)
        {
            IsError = true;
            ErrorMessage = message;
        }

        /// <summary>
        /// Create a instance at wrong response
        /// </summary>
        /// <param name="message">Message text error</param>
        /// <param name="error">Code error from <see cref="Enums.Error"/></param>
        public ResponseDto(string message, Enums.Error error)
        {
            IsError = true;
            ErrorMessage = message;
            ErrorCode = (int)error;
        }

        /// <summary>
        /// Create a instance at exception error throw
        /// </summary>
        /// <param name="message">Message text error</param>
        /// <param name="exception">Exception throw object</param>
        public ResponseDto(string message, Exception exception)
        {
            IsError = true;
            ErrorMessage = message;
            Exception = exception;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Represent throw error code
        /// </summary>
        public int ErrorCode { get; set; } = (int)Enums.Error.UnknowError;

        /// <summary>
        /// Message error response
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// Flag to error
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Generic object to response
        /// </summary>
        public T Response { get; set; }

        /// <summary>
        /// Exception throw object
        /// </summary>
        public Exception Exception { get; set; }
        #endregion
    }
}
