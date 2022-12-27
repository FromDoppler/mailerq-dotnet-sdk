using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    /// <summary>
    /// An Internet Protocol Address of one Pool
    /// MailerQ offers IP Pools for easy management of your sending IP addresses.
    /// </summary>
    /// <remarks>Get method requiere indicate the pool name as request parameter</remarks>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class PoolIP : IRestApiModel
    {
        /// <summary>
        /// The IP address
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string LocalIp { get; set; }
        /// <summary>
        /// The name of the pool
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}
