using Newtonsoft.Json;
using System.Collections.Generic;

namespace MailerQ
{
    /// <summary>
    /// The actions to do by MailerQ with the MIME headers
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class MimeHeaders
    {
        /// <summary>
        /// The headers to remove
        /// </summary>
        public ICollection<string> Remove { get; set; }

        /// <summary>
        /// The headers to prepend before any existing header
        /// </summary>
        public IDictionary<string, object> Prepend { get; set; }

        /// <summary>
        /// The headers to append after all existing header
        /// </summary>
        public IDictionary<string, object> Append { get; set; }

        /// <summary>
        /// The headers to replace
        /// </summary>
        public IDictionary<string, object> Replace { get; set; }

        /// <summary>
        /// The headers to update the value
        /// </summary>
        public IDictionary<string, object> Update { get; set; }
    }
}
