namespace MailerQ
{
    public class MailerQConfiguration
    {
        public string RabbitConnectionString { get; set; }

        public string QueuesNamePrefix { get; set; }

        public string MessageStorageUrl { get; set; }
    }
}
