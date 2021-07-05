namespace MailerQ.Conventions
{
    /// <summary>
    /// DKIM signed form of canonicalization
    /// </summary>
    public static class Canonicalization
    {
        /// <summary>
        /// Simple for headers and simple for body
        /// </summary>
        public const string SimpleSimple = "simple/simple";

        /// <summary>
        /// Simple for headers and relaxed for body
        /// </summary>
        public const string SimpleRelaxed = "simple/relaxed";

        /// <summary>
        /// Relaxed for headers and simple for body
        /// </summary>
        public const string RelaxedSimple = "relaxed/simple";

        /// <summary>
        /// Relaxed for heades and relaxed for body
        /// </summary>
        public const string RelaxedRelaxed = "relaxed/relaxed";

        /// <summary>
        /// The default canonicalization used by MailerQ
        /// </summary>
        public static string Default => RelaxedSimple;
    }
}
