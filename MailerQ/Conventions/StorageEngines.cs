namespace MailerQ.Conventions
{
    /// <summary>
    /// Message store supported by MailerQ
    /// </summary>
    public enum StorageEngines
    {
        /// <summary>
        /// MongoDB document database
        /// </summary>
        MongoDB,
        /// <summary>
        /// CouchBase document database
        /// </summary>
        CouchBase,
        /// <summary>
        /// MySql relational database
        /// </summary>
        MySql,
        /// <summary>
        /// PostgreSql relational database
        /// </summary>
        PostgreSql,
        /// <summary>
        /// SQlite relational database
        /// </summary>
        SQlite,
        /// <summary>
        /// Directory on file system
        /// </summary>
        Directory,
        /// <summary>
        /// Amazon simple storage service
        /// </summary>
        S3,
    }
}
