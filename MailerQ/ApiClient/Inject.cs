using System;
using System.Collections.Generic;
using System.Text;
using MailerQ.Mime;
using Newtonsoft.Json;

namespace MailerQ.ApiClient
{
    [JsonObject(
    NamingStrategyType = typeof(LowercaseNamingStrategy),
    ItemNullValueHandling = NullValueHandling.Ignore
)]
    public class Inject : OutgoingMessage
    {
        /// <summary>
        /// the priority of the message
        /// </summary>
        public Http Http { get; set; }
    }
}
