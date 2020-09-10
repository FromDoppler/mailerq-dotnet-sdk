namespace MailerQ
{
    /// <summary>
    /// Sent over the same connection information
    /// </summary>
    public class SentInfo
    {
        /// <summary>
        /// Amount of bytes sent over the same connection
        /// </summary>
        public int Connection { get; set; }
        /// <summary>
        /// Amount of bytes of MIME (no SMTP commands) sent over the same connection
        /// </summary>
        public int Data { get; set; }
    }
}
