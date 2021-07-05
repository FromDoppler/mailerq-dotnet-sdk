using Newtonsoft.Json.Serialization;

namespace MailerQ
{
    // TODO: this should be in a more generic place and reuse in other projects
    /// <summary>
    /// Serialize property names and dictionary keys as a lower case invariant string
    /// </summary>
    public class LowercaseNamingStrategy : NamingStrategy
    {
        /// <inheritdoc />
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLowerInvariant();
        }
    }
}
