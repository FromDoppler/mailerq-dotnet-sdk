using System;
using System.Collections.Generic;
using System.Text;
using MailerQ.Mime;
using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    [JsonObject(
    NamingStrategyType = typeof(LowercaseNamingStrategy),
    ItemNullValueHandling = NullValueHandling.Ignore
)]
    public class Inject : OutgoingMessage
    {
        /// <summary>
        /// Endpoint name
        /// </summary>
        public readonly string Endpoint = "inject";

        /// <summary>
        /// The priority of the message
        /// </summary>
        public Http Http { get; set; }
    }
}
