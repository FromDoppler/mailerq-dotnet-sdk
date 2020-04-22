using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailerQ.ApiClient
{
    [JsonObject(
    NamingStrategyType = typeof(LowercaseNamingStrategy),
    ItemNullValueHandling = NullValueHandling.Ignore
)]
    public class Error
    {
        /// <summary>
        /// Endpoint name
        /// </summary>
        public readonly string Endpoint = "errors";

        /// <summary>
        /// Numeric error code between 200 and 599 (smtp error codes).
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Extended SMTP error code, e.g. 5.7.1.
        /// </summary>
        public string Extended { get; set; }

        /// <summary>
        /// Description to put in the message result.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Pool that the error applies to.
        /// </summary>
        public string Pool { get; set; }

        /// <summary>
        /// MTA IP that the error applies to.
        /// </summary>
        public string Mta { get; set; }

        /// <summary>
        /// Domain that the error applies to.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// The tag that the error applies to.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Whether or not this error is for the entire cluster or only this instance.
        /// </summary>
        public bool Cluster { get; set; }
    }
}
