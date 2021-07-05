using Newtonsoft.Json;

namespace MailerQ
{
    /// <summary>
    /// Custom result queues
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Queues
    {
        /// <summary>
        /// All result queue name
        /// </summary>
        /// <remarks>Set with string "null" to discart the result message or keep as default value to use the name from MailerQ config</remarks>
        public string Results { get; set; }

        /// <summary>
        /// Failure result queue name
        /// </summary>
        /// <remarks>Set with string "null" to discart the result message or keep as default value to use the name from MailerQ config</remarks>
        public string Failure { get; set; }

        /// <summary>
        /// Success result queue name
        /// </summary>
        /// <remarks>Set with string "null" to discart the result message or keep as default value to use the name from MailerQ config</remarks>
        public string Success { get; set; }

        /// <summary>
        /// Retry result queue name
        /// </summary>
        /// <remarks>Set with string "null" to discart the result message or keep as default value to use the name from MailerQ config</remarks>
        public string Retry { get; set; }

        /// <summary>
        /// DSN result queue name
        /// </summary>
        /// <remarks>Set with string "null" to discart the result message or keep as default value to use the name from MailerQ config</remarks>
        public string Dsn { get; set; }
    }
}
