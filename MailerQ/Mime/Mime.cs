using Newtonsoft.Json;
using System.Collections.Generic;

namespace MailerQ.Mime
{
    /// <summary>
    /// This represent a Mime json message
    /// See https://www.responsiveemail.com/json/introduction for more info
    /// </summary>
    [JsonObject(
        NamingStrategyType = typeof(LowercaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Mime
    {
        #region Top level MIME properties

        /// <summary>
        /// Subject line of the email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Email address and name of the sender.
        /// </summary>
        public EmailAddress From { get; set; }

        /// <summary>
        /// Optional email address and name of the user to which replies are sent.
        /// </summary>
        [JsonProperty("replyTo")]
        public EmailAddress ReplyTo { get; set; }

        /// <summary>
        /// List of receivers.
        /// </summary>
        [JsonConverter(typeof(SingleOrArrayConverter<EmailAddress>))]
        public ICollection<EmailAddress> To { get; set; }

        /// <summary>
        /// List of CC addresses.
        /// </summary>
        [JsonConverter(typeof(SingleOrArrayConverter<EmailAddress>))]
        public ICollection<EmailAddress> Cc { get; set; }

        /// <summary>
        /// List of BCC addresses.
        /// </summary>
        [JsonConverter(typeof(SingleOrArrayConverter<EmailAddress>))]
        public ICollection<EmailAddress> Bcc { get; set; }

        /// <summary>
        /// Additional custom headers to be added to the mail header.
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Attachments to download and add to the email.
        /// </summary>
        public ICollection<Attachment> Attachments { get; set; }

        /// <summary>
        /// Private key for DKIM signature.
        /// </summary>
        public object Dkim { get; set; }

        #endregion

        #region Top level content and style properties

        /// <summary>
        /// Supply text version for clients that do not support HTML emails
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Template wide font and text settings.
        /// </summary>
        public object Font { get; set; }

        /// <summary>
        /// Background settings for the entire template.
        /// </summary>
        public object Background { get; set; }

        /// <summary>
        /// The main block that holds all of the other blocks and content of the responsive email.
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// Custom CSS settings to be added to the <body> tag.
        /// </summary>
        public object Css { get; set; }

        /// <summary>
        /// Custom attributes to be added to the <body> tag.
        /// </summary>
        public object Attributes { get; set; }

        #endregion

        #region Top level advanced properties

        /// <summary>
        /// Define specific rules to overwrite information specified in the JSON.
        /// </summary>
        public object Rewrite { get; set; }

        /// <summary>
        /// Supply email tracking information.
        /// </summary>
        public object Tracking { get; set; }

        /// <summary>
        /// Supply data used for personalization
        /// </summary>
        public object Data { get; set; }

        #endregion

    }
}
