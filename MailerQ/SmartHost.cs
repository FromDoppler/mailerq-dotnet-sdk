using Newtonsoft.Json;

namespace MailerQ
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class SmartHost
    {
        public string Hostname { get; set; }

        public int Port { get; set; } = 25;

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
