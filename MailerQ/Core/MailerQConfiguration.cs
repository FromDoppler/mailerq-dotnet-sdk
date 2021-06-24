﻿namespace MailerQ
{
    public class MailerQConfiguration
    {
        public string RabbitConnectionString { get; set; }

        /// <summary>
        /// Password for connect to RabbitMQ. 
        /// If RabbitConnectionString has defined password parameter, will be replaced with this value if it is not empty.
        /// </summary>
        public string RabbitPassword { get; set; }

        public string QueuesNamePrefix { get; set; }

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
