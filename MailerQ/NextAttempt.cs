using Newtonsoft.Json;
using System;

namespace MailerQ
{
    /// <summary>
    /// The moment when MailerQ will do the next delivery attempt
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class NextAttempt
    {
        /// <summary>
        /// The time of the next delivery attempt
        /// </summary>
        public DateTime Time { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Time.ToString();
        }
    }
}
