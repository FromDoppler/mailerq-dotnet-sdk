using Newtonsoft.Json.Converters;

namespace MailerQ.Json
{
    /// <summary>
    /// Converter to serialize date time with format used by MailerQ
    /// </summary>
    public class DateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Constructor of converter for MailerQ date time format
        /// </summary>
        public DateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
