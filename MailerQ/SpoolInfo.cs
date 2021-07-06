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
        /// <summary>
        /// The directory path
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// The file name
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// The file user owner
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// The file size
        /// </summary>
        public int Size { get; set; }
    }
}
