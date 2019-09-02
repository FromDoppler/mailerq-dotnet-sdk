using Newtonsoft.Json.Serialization;

namespace MailerQ
{
    // TODO: this should be in a more generic place and reuse in other projects
    public class LowercaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLowerInvariant();
        }
    }
}
