using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    /// <summary>
    /// The protocol sopported to connect to the external server
    /// </summary>
    public enum ExternalIpProtocol
    {
        /// <summary>
        /// Network address translation
        /// </summary>
        Nat
    }

    /// <summary>
    /// External MTA.
    /// Setting to uses IP address that is not local to the server MailerQ is running on.
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class ExternalIP
    {
        /// <summary>
        /// The external IP address to use for sending
        /// </summary>
        [JsonProperty("public_ip", Required = Required.Always)]
        public string PublicIP { get; set; }
        /// <summary>
        /// The local IP address to bind to
        /// </summary>
        [JsonProperty("local_ip")]
        public string LocalIP { get; set; }
        /// <summary>
        /// The port to use for the external server
        /// </summary>
        [JsonProperty("connect_port")]
        public int ConnectPort { get; set; }
        /// <summary>
        /// The protocol to use to connect to the external server(currently only 'nat' is supported)
        /// </summary>
        public ExternalIpProtocol Protocol { get; set; }
    }
}
