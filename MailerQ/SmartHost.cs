using Newtonsoft.Json;

namespace MailerQ
{
    /// <summary>
    /// An alternative SMTP server on the internet to send the message
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class SmartHost
    {
        /// <summary>
        /// The smart host name
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// The smart host port
        /// </summary>
        public int Port { get; set; } = 25;

        /// <summary>
        /// Username for authenticate with the smart host if requiered
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password of the user for authenticate with the smart host if requiered
        /// </summary>
        public string Password { get; set; }
    }
}
