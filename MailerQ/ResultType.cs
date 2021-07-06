namespace MailerQ
{
    /// <summary>
    /// Result type of delivery attempt
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Message was accepted (only reported in combination with state "message")
        /// </summary>
        Accepted,
        /// <summary>
        /// No answer was received in time
        /// </summary>
        Timeout,
        /// <summary>
        /// Connection was lost while waiting for an answer
        /// </summary>
        Lost,
        /// <summary>
        /// An answer was received, but the answer contained a fatal error message
        /// </summary>
        Error,
        /// <summary>
        /// An answer was received, but it could not be recognized as valid answer
        /// </summary>
        Invalid
    }
}
