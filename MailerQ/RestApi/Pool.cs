using Newtonsoft.Json;
using System;

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
    public class Pool : IRestApiModel
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
    /// Pool of IPs
    /// MailerQ offers IP Pools for easy management of your sending IP addresses.
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    [Obsolete("User Pool class")]
    public class IpPool : Pool { }
}
