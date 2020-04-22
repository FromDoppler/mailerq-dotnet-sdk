using System;
using System.Collections.Generic;
using System.Text;

namespace MailerQ.ApiClient
{
    /// <summary>
    /// Property of injected message that hold information about who connected and the name of the authentication token.
    /// </summary>
    public class Http
    {
        /// <summary>
        /// Name of the authentication token
        /// </summary>
        public string Authentication { get; set; }

        /// <summary>
        /// Remote ip who connected
        /// </summary>
        public string RemoteIp { get; set; }

        /// <summary>
        /// Remote port who connected
        /// </summary>
        public string RemotePort { get; set; }
    }
}
