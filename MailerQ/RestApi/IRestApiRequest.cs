namespace MailerQ.RestApi
{
    /// <summary>
    /// Contract that represent a basic MailerQ Rest API request
    /// </summary>
    /// <typeparam name="T">Model type that accept the endpoint</typeparam>
    public interface IRestApiRequest<T> where T : IRestApiModel
    {
        /// <summary>
        /// The query string parameters
        /// </summary>
        string QueryString { get; }
    }
}
