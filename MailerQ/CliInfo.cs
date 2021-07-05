using Newtonsoft.Json;
using System.Collections.Generic;

namespace MailerQ
{
    /// <summary>
    /// Command line interface
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class CliInfo
    {
        /// <summary>
        /// Binary full path of the excuted command
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Collection of arguments passed to the command
        /// </summary>
        public ICollection<string> Arguments { get; set; }

        /// <summary>
        /// User that execute the command
        /// </summary>
        public string User { get; set; }
    }
}
