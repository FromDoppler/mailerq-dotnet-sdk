using System;
using System.Collections.Generic;
using System.Text;

namespace MailerQ.Conventions
{
    public static class Canonicalization
    {
        public const string SimpleSimple = "simple/simple";
        public const string SimpleRelaxed = "simple/relaxed";
        public const string RelaxedSimple = "relaxed/simple";
        public const string RelaxedRelaxed = "relaxed/relaxed";

        public static string Default => RelaxedSimple;
    }
}
