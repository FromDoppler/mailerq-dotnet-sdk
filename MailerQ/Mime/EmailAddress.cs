using Newtonsoft.Json;

namespace MailerQ.Mime
{
    /// <summary>
    /// The Email Address
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class EmailAddress
    {
        /// <summary>
        /// The email address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The visible address
        /// </summary>
        public string Name { get; set; }
    }
}
