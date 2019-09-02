using Newtonsoft.Json;

namespace MailerQ
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Queues
    {
        public string Results { get; set; }
        public string Failure { get; set; }
        public string Success { get; set; }
        public string Retry { get; set; }
        public string Dsn { get; set; }
    }
}
