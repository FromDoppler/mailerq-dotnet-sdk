namespace MailerQ
{
    public enum DeliveryState
    {
        /// <summary>
        /// Unknown and unexpect state
        /// </summary>
        Unknown,
        /// <summary>
        /// The input is checked and assigned to the from and to ip address between which it will be sent
        /// </summary>
        Process,
        /// <summary>
        ///  The mime data is loaded from external nosql storage
        /// </summary>
        Storage,
        /// <summary>
        /// The json message is converted into a responsive email
        /// </summary>
        Responsive,
        /// <summary>
        /// The message is personalized if personalization data is available
        /// </summary>
        Personalize,
        /// <summary>
        /// The hostname to which the mail should be sent is looked up in DNS
        /// </summary>
        Dns,
        /// <summary>
        /// A local IP address is chosen from which the mail is going to be sent
        /// </summary>
        Bind,
        /// <summary>
        /// A tcp connection is set up
        /// </summary>
        Connect,
        /// <summary>
        ///  The TCP connection has been set up, waiting for initial welcome message from server
        /// </summary>
        Intro,
        /// <summary>
        /// The "EHLO" message has been sent
        /// </summary>
        Ehlo,
        /// <summary>
        /// the "HELO" message has been sent
        /// </summary>
        Helo,
        /// <summary>
        /// the "STARTTLS" message has been sent
        /// </summary>
        Starttls,
        /// <summary>
        /// The "AUTH PLAIN" message has been sent
        /// </summary>
        AuthPlain,
        /// <summary>
        /// The "AUTH LOGIN" message has been sent
        /// </summary>
        AuthLogin,
        /// <summary>
        /// the username for authentication has been sent
        /// </summary>
        AuthUsername,
        /// <summary>
        /// The password for authentication has been sent
        /// </summary>
        AuthPassword,
        /// <summary>
        /// The "AUTH CRAM-MD5" message has been sent
        /// </summary>
        AuthCram,
        /// <summary>
        /// The response to the cram-md5 challenge has been sent
        /// </summary>
        AuthResponse,
        /// <summary>
        /// The "MAIL FROM" message has been sent
        /// </summary>
        MailFrom,
        /// <summary>
        /// The "RCPT TO" message has been sent
        /// </summary>
        RcptTo,
        /// <summary>
        /// The "DATA" message has been sent
        /// </summary>
        Data,
        /// <summary>
        /// The full mime data followed by a dot has been sent
        /// </summary>
        Message,
        /// <summary>
        /// Email was loaded in an event loop that never becomes idle
        /// </summary>
        Idle,
    }
}
