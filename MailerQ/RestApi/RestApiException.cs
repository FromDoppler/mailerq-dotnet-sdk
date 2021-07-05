using System;

namespace MailerQ.RestApi
{
    /// <summary>
    /// Represents errors that occur executing the Rest API Client.
    /// </summary>
    [Serializable]
    public class RestApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MailerQ.RestApi.RestApiException class.
        /// </summary>
        public RestApiException() { }

        /// <summary>
        /// Initializes a new instance of the MailerQ.RestApi.RestApiException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RestApiException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the MailerQ.RestApi.RestApiException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
        /// </param>
        public RestApiException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the MailerQ.RestApi.RestApiException class with a specified error message.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.
        /// </param>
        protected RestApiException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
