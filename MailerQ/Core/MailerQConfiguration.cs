namespace MailerQ
{
    /// <summary>
    /// Configuration required by the SDK to interact with MailerQ and its backend services
    /// </summary>
    public class MailerQConfiguration
    {
        /// <summary>
        /// RabbitMQ AMPQ connection string
        /// </summary>
        public string RabbitConnectionString { get; set; }

        /// <summary>
        /// Password for connect to RabbitMQ. 
        /// If RabbitConnectionString has defined password parameter, will be replaced with this value if it is not empty.
        /// </summary>
        public string RabbitPassword { get; set; }

        /// <summary>
        /// A prefix for add to the queues name
        /// </summary>
        public string QueuesNamePrefix { get; set; }

        /// <summary>
        /// The address URI of the MIME message store
        /// </summary>
        /// <remarks><see cref="Conventions.StorageEngines"/> have the list of MailerQ possible backing storage engines </remarks>
        public string MessageStorageUrl { get; set; }

        /// <summary>
        /// The base url of the MailerQ Rest Api instance
        /// </summary>
        public string RestApiUrl { get; set; }

        /// <summary>
        /// The authorization token to use MailerQ Rest Api
        /// </summary>
        public string RestApiToken { get; set; }
    }
}
