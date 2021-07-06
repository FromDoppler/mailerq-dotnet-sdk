namespace MailerQ.Conventions
{
    /// <summary>
    /// Default setting for MailerQ queue name
    /// </summary>
    public static class QueueName
    {
        /// <summary>
        /// Default name for Outbox queue
        /// </summary>
        public const string Outbox = "outbox";

        /// <summary>
        /// Default name for Inbox queue
        /// </summary>
        public const string Inbox = "inbox";

        /// <summary>
        /// Default name for Reports queue
        /// </summary>
        public const string Reports = "reports";

        /// <summary>
        /// Default name for Local queue
        /// </summary>
        public const string Local = "local";

        /// <summary>
        /// Default name for Refused queue
        /// </summary>
        public const string Refused = "refused";

        /// <summary>
        /// Default name for DSN queue
        /// </summary>
        public const string DSN = "dsn";

        /// <summary>
        /// Default name for Results queue
        /// </summary>
        public const string Results = "results";

        /// <summary>
        /// Default name for Success queue
        /// </summary>
        public const string Success = "success";

        /// <summary>
        /// Default name for Failure queue
        /// </summary>
        public const string Failure = "failure";

        /// <summary>
        /// Default name for Retry queue
        /// </summary>
        public const string Retry = "retry";
    }
}
