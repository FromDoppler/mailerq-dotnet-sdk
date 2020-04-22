using Newtonsoft.Json;

namespace MailerQ.RestApi
{
    /// <summary>
    /// Using this endpoint MailerQ allows you to pause almost any combination of pool/mta and domain/ip.
    /// </summary>
    [JsonObject(
       NamingStrategyType = typeof(LowercaseNamingStrategy),
       ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Pause
    {
        /// <summary>
        /// Pool that the pause applies to.
        /// </summary>
        public string Pool { get; set; }

        /// <summary>
        /// MTA IP that the pause applies to.
        /// </summary>
        public string Mta { get; set; }

        /// <summary>
        /// Domain that the pause applies to.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Remote IP that the pause applies to.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// The tag that the pause applies to.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Whether or not this pause is for the entire cluster or only this instance.
        /// </summary>
        public bool Cluster { get; set; }
    }
}
