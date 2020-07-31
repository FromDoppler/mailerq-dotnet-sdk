using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    /// <summary>
    /// Pool of IPs
    /// MailerQ offers IP Pools for easy management of your sending IP addresses.
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class IpPool
    {
        /// <summary>
        /// The name of the pool
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the pool, for human use.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// IP Address
    /// MailerQ offers IP Pools for easy management of your sending IP addresses.
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class IpAddress
    {
        /// <summary>
        /// The IP address
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Ip { get; set; }
        /// <summary>
        /// The name of the pool
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

    }
}
