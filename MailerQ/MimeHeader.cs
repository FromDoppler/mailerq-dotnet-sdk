using Newtonsoft.Json;
using System.Collections.Generic;

namespace MailerQ
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class MimeHeaders
    {
        public ICollection<string> Remove { get; set; }
        public IDictionary<string, object> Prepend { get; set; }
        public IDictionary<string, object> Append { get; set; }
        public IDictionary<string, object> Replace { get; set; }
        public IDictionary<string, object> Update { get; set; }
    }
}
