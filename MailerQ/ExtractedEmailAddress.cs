namespace MailerQ
{
    /// <summary>
    /// A email address extracted from the MIME message
    /// </summary>
    public class ExtractedEmailAddress
    {
        /// <summary>
        /// The domain part of the email address
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// The local part of the email address
        /// </summary>
        public string Local { get; set; }
    }
}
