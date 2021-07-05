using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    /// <summary>
    /// Represent a model of OutgoingMessage to inject in the MailerQ Rest API
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Inject : OutgoingMessage, IRestApiModel
    {
    }
}
