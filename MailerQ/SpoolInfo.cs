using Newtonsoft.Json;

namespace MailerQ
{
    /// <summary>
    /// Spool directory data
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class SpoolInfo
    {
        public string Directory { get; set; }

        public string File { get; set; }

        public string User { get; set; }

        public int Size { get; set; }
    }
}
