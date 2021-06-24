using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Inject : OutgoingMessage
    {
        /// <summary>
        /// Endpoint name
        /// </summary>
        public readonly string Endpoint = "inject";
    }
}
