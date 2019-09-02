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
        public string Command { get; set; }

        public ICollection<string> Arguments { get; set; }

        public string User { get; set; }
    }
}
