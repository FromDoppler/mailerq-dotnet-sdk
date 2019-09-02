using Newtonsoft.Json;

namespace MailerQ.Mime
{
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public partial class Attachment
    {
        /// <summary>
        /// Url to your data, this will be downloaded and included
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The raw data of your attachment, this has to be base64 encoded
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The name for your attachment, this will be visibile in most email clients
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The content type for this file, this is ignored in case you provide your attachment by url as it'll look at the http headers
        /// </summary>
        public string Type { get; set; }
    }
}
