using Newtonsoft.Json;
using System;

namespace MailerQ
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class NextAttempt
    {
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return Time.ToString();
        }
    }
}
