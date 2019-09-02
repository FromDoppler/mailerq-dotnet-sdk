using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailerQ
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Dkim
    {
        public string Domain { get; set; }

        public string Selector { get; set; }

        public string Key { get; set; }

        public DateTime? Expire { get; set; }

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
    }
}
