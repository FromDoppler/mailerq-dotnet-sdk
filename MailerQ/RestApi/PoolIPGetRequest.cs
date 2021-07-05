namespace MailerQ.RestApi
{
    /// <summary>
    /// A rest API GET request specific for PoolIP endpoint
    /// </summary>
    public class PoolIPGetRequest : IRestApiRequest<PoolIP>
    {
        private readonly string poolName;

        /// <summary>
        /// Initializes a new instance of GET request specific for PoolIP endpoint
        /// </summary>
        /// <param name="poolName">The Pool name of IPs</param>
        public PoolIPGetRequest(string poolName)
        {
            this.poolName = poolName;
        }

        /// <inheritdoc />
        public string QueryString => $"name={poolName}";
    }
}
