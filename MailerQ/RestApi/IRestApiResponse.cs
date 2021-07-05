using System.Collections.Generic;
using System.Net;

namespace MailerQ.RestApi
{
    /// <summary>
    /// Contract that represent a MailerQ Rest API response
    /// </summary>
    /// <typeparam name="T">Model type that return the endpoint</typeparam>
    public interface IRestApiResponse<T> where T : IRestApiModel
    {
        /// <summary>
        /// Response HTTP status code
        /// </summary>
        HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Collection of object deserialized from the response content
        /// </summary>
        ICollection<T> Content { get; }
    }
}
