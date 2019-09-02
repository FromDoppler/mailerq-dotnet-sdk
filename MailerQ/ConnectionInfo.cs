using Newtonsoft.Json;

namespace MailerQ
{
    /// <summary>
    /// TCP connection data
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class ConnectionInfo
    {
        [JsonProperty("local-ip")]
        public string LocalIP { get; set; }

        [JsonProperty("local-port")]
        public int LocalPort { get; set; }

        [JsonProperty("remote-ip")]
        public string RemoteIP { get; set; }

        [JsonProperty("remote-port")]
        public int RemotePort { get; set; }

        public bool Secure { get; set; }

        public string User { get; set; }
    }
}
