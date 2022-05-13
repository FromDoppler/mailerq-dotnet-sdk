using Newtonsoft.Json.Converters;

namespace MailerQ.Json
{
    /// <summary>
    /// Converter to serialize date time with format used by MailerQ
    /// </summary>
    public class MailerQDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Constructor of converter for MailerQ date time format
        /// </summary>
        public MailerQDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
