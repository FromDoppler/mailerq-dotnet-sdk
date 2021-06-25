using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.RestApi
{
    /// <summary>
    /// Contract that represent the HTTP methods availables in the MailerQ Rest API
    /// </summary>
    public interface IRestApiClient
    {
        /// <summary>
        /// Request an HTTP Get to obtain a collection of instances of the model
        /// </summary>
        /// <typeparam name="T">Model type that return the endpoint</typeparam>
        /// <param name="restApiRequest">Specific definition of request the Rest API</param>
        /// <param name="cancellationToken">The request cancellationToken</param>
        /// <returns>Response information returned on the request including the collection of instances of the model</returns>
        Task<IRestApiResponse<T>> Get<T>(
            IRestApiRequest<T> restApiRequest = default,
            CancellationToken cancellationToken = default) where T : IRestApiModel;

        /// <summary>
        /// Request an HTTP Post create or update an instance of the model
        /// </summary>
        /// <typeparam name="T">Model type that accept the endpoint</typeparam>
        /// <param name="model">Object model to send in the request body</param>
        /// <param name="cancellationToken">The request cancellationToken</param>
        /// <returns>Response information returned on the request</returns>
        Task<IRestApiResponse<T>> Post<T>(T model, CancellationToken cancellationToken = default) where T : IRestApiModel;

        /// <summary>
        /// Request an HTTP Delete remove an instance of the model
        /// </summary>
        /// <typeparam name="T">Model type that accept the endpoint</typeparam>
        /// <param name="model">Object model to send in the request body</param>
        /// <param name="cancellationToken">The request cancellationToken</param>
        /// <returns>Response information returned on the request</returns>
        Task<IRestApiResponse<T>> Delete<T>(T model, CancellationToken cancellationToken = default) where T : IRestApiModel;
    }
}
