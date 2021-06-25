using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Inject : OutgoingMessage, IRestApiModel
    {
    }
}
