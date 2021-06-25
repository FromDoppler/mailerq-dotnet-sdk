using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    /// <summary>
    /// Suppression Types
    /// </summary>
    public enum SuppressionTypes
    {
        /// <summary>
        /// Suppress a full address
        /// </summary>
        Address,
        /// <summary>
        /// Suppress a domain
        /// </summary>
        Domain
    }

    /// <summary>
    /// Suppression is functionality to prevent unwanted recipients from being tried and protect reputation.
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Suppression : IRestApiModel
    {
        /// <summary>
        /// The domain or address to be suppressed
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Value { get; set; }

        /// <summary>
        /// ("address", "domain")	Whether the given value is a domain or a full address
        /// </summary>
        public SuppressionTypes Type { get; set; }

        /// <summary>
        /// The error code given to suppressed messages.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Extended SMTP error code, e.g. 5.7.1.
        /// </summary>
        public string Extended { get; set; }

        /// <summary>
        /// Description to put in the message result.
        /// </summary>
        public string Description { get; set; }
    }
}
