﻿using EasyNetQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailerQ
{
    /// <summary>
    /// Incoming messages received by MailerQ
    /// </summary>
    [Queue(Conventions.QueueName.Inbox)]
    [JsonObject(
    NamingStrategyType = typeof(LowercaseNamingStrategy),
    ItemNullValueHandling = NullValueHandling.Ignore
)]
    public class IncomingMessage : OutgoingMessage
    {
        /// <summary>
        /// server name that received the message
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// Time when the mail was received
        /// </summary>
        public DateTime Received { get; set; }

        /// <summary>
        /// Connection info (only used when received via smtp)
        /// </summary>
        public ConnectionInfo Connection { get; set; }

        /// <summary>
        /// Spool info (only used when received from spool dir)
        /// </summary>
        public SpoolInfo Spool { get; set; }

        /// <summary>
        /// Command info (only used when received from cli)
        /// </summary>
        public CliInfo Cli { get; set; }

        /// <summary>
        /// Array with the results of the SPF, DKIM , DMARC and SPAM checks*/
        /// </summary>
        // TODO: verify if Checks should be a more specify class insted object
        public ICollection<object> Checks { get; set; }

        // TODO: verify if MTA and Report should be part of OutgoingMessage

        /// <summary>
        /// The Mail Transfer Agent that the message comes from
        /// </summary>
        public string MTA { get; set; }

        /// <summary>
        /// If the message require a DSN report
        /// </summary>
        public bool Report { get; set; }

        /// <summary>
        /// Rest API info (only used when received from Rest Api)
        /// </summary>
        public RestApiInfo Http { get; set; }
    }

    /// <summary>
    /// <inheritdoc/> holding a delivery report
    /// </summary>
    [Queue(Conventions.QueueName.Reports)]
    public class ReportMessage : IncomingMessage
    {
    }

    /// <summary>
    /// Outgoing Delivery Status Notification messages
    /// </summary>
    [Queue(Conventions.QueueName.DSN)]
    public class DSNMessage : IncomingMessage
    {
        // FIX: change the base type to Outgoing message
    }

    /// <summary>
    /// <inheritdoc/> that match a local address
    /// </summary>
    [Queue(Conventions.QueueName.Local)]
    public class LocalMessage : IncomingMessage
    {
    }
}
