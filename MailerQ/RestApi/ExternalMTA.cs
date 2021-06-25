using Newtonsoft.Json;
using System;

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
        Nat,
        /// <summary>
        /// Socks
        /// </summary>
        Socks,
        /// <summary>
        /// Http
        /// </summary>
        Http,
    }

    /// <summary>
    /// External MTA.
    /// Setting to uses IP address that is not local to the server MailerQ is running on.
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    [Obsolete("User ExternalMTA class")]
    public class ExternalIp : ExternalMTA { }

    /// <summary>
    /// External MTA.
    /// Setting to uses IP address that is not local to the server MailerQ is running on.
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class ExternalMTA : IRestApiModel
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
        /// The IP to use for the external server
        /// </summary>
        [JsonProperty("connect_ip")]
        public string ConnectIP { get; set; }

        /// <summary>
        /// The port to use for the external server
        /// </summary>
        [JsonProperty("connect_port")]
        public int ConnectPort { get; set; }

        /// <summary>
        /// The protocol to use to connect to the external server
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ExternalIpProtocol Protocol { get; set; }
    }
}
