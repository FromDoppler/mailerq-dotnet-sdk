using System.Collections.Generic;
using System.Net;

namespace MailerQ.RestApi
{
    /// <summary>
    /// A generic MailerQ Rest API response
    /// </summary>
    /// <typeparam name="T">Model type that return the endpoint</typeparam>
    public class RestApiResponse<T> : IRestApiResponse<T> where T : IRestApiModel
    {
        /// <inheritdoc />
        public HttpStatusCode HttpStatusCode { get; }

        /// <inheritdoc />
        public ICollection<T> Content { get; }

        /// <summary>
        /// Initializes a new instance of generic MailerQ Rest API response
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code of the response</param>
        /// <param name="content">Collection of object deserialized from the response content</param>
        public RestApiResponse(HttpStatusCode httpStatusCode, ICollection<T> content = default)
        {
            HttpStatusCode = httpStatusCode;
            Content = content;
        }
    }
}
