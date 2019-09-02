namespace MailerQ.Conventions
{
    public static class QueueName
    {
        public const string Outbox = "outbox";

        public const string Inbox = "inbox";

        public const string Reports = "reports";

        public const string Local = "local";

        public const string Refused = "refused";

        public const string DSN = "dsn";

        public const string Results = "results";

        public const string Success = "success";

        public const string Failure = "failure";

        public const string Retry = "retry";
    }
}
