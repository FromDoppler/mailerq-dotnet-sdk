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
        /// <summary>
        /// The local IP address of the connection
        /// </summary>
        [JsonProperty("local-ip")]
        public string LocalIP { get; set; }

        /// <summary>
        /// The local port of the connection
        /// </summary>
        [JsonProperty("local-port")]
        public int LocalPort { get; set; }

        /// <summary>
        /// The remote IP address of the connection
        /// </summary>
        [JsonProperty("remote-ip")]
        public string RemoteIP { get; set; }

        /// <summary>
        /// The remote port of the connection
        /// </summary>
        [JsonProperty("remote-port")]
        public int RemotePort { get; set; }

        /// <summary>
        /// If the TCP connection is secure and some sort of SMTP authentication mechanism was used
        /// </summary>
        public bool Secure { get; set; }

        /// <summary>
        /// The username of the user who submitted the message when the TCP connection is secure and some sort of SMTP authentication mechanism was used, 
        /// </summary>
        public string User { get; set; }
    }
}
