using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailerQ
{
    /// <summary>
    /// Private DomainKeys Identified Mail
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Dkim
    {
        /// <summary>
        /// The domain of the keys
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Selector to perform a DNS lookup
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// RSA private keys
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// When the Dkim expire
        /// </summary>
        [JsonConverter(typeof(Json.DateTimeConverter))]
        public DateTime? Expire { get; set; }

        /// <summary>
        /// Extra custom headers to sign
        /// </summary>
        public List<string> Headers { get; set; }

        /// <summary>
        /// If you want to receive reports from remote servers whenever a DKIM signature fails to verify
        /// </summary>
        public bool? Report { get; set; }

        /// <summary>
        /// This option should be an array of the protocols you want to use (DKIM and/or ARC)
        /// </summary>
        public ICollection<string> Protocols { get; set; }

        /// <summary>
        /// A DKIM can be signed using different forms of canonicalization. Default is relaxed/simple.
        /// </summary>
        public string Canonicalization { get; set; }

        /// <summary>
        /// Oversign headers
        /// </summary>
        public bool? Oversign { get; set; }
    }
}
