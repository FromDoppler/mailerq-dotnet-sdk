using System;

namespace MailerQ
{
    public class DeliveryAttempt
    {
        /// <summary>
        /// State in the delivery where the error occured
        /// </summary>
        public DeliveryState State { get; set; }

        /// <summary>
        /// Type or result(accepted, timeout, lost, error, invalid)
        /// </summary>
        public ResultType Result { get; set; }

        /// <summary>
        /// Time of the result
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Name of the receiving mta(reported when the connection was set up)
        /// </summary>
        public string Mta { get; set; }

        /// <summary>
        /// IP from which the mail was sent
        /// </summary>
        public string From { get; set; }

        /// <summary>
        ///IP to which the mail was sent
        /// </summary>
        public string To { get; set; }

        /// <summary>
        ///If authentication/login was necessary for access, the authentication protocol used
        /// </summary>
        public string Authentication { get; set; }

        /// <summary>
        /// The encryption protocol used(if the message was sent over a secure connection)
        /// </summary>
        public object Cipher { get; set; }

        /// <summary>
        /// Information about the ssl certificate that was issued by the receiving server
        /// </summary>
        public object Certificate { get; set; }

        /// <summary>
        /// Number of messages sent over the same connection
        /// </summary>
        public int Message { get; set; }

        /// <summary>
        /// SMTP result code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Extended SMTP status code
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Answer received from receiving server
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Did the remote server implement the dsn protocol?
        /// </summary>
        public bool Dsn { get; set; }

        public int Attempt { get; set; }

        public SentInfo Sent { get; set; }
    }
}
