using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.MessageStore
{
    /// <summary>
    /// Contract that represent a message store of the MailerQ
    /// </summary>
    public interface IMessageStorage
    {
        /// <summary>
        /// Insert a message in the storage
        /// </summary>
        /// <param name="message">The message to insert</param>
        /// <param name="secondsToExpire">The time in seconds to expire the message and remove it from the storage</param>
        /// <param name="cancellationToken">The cancellation token for the taks</param>
        /// <returns></returns>
        Task<string> InsertAsync(string message, int secondsToExpire = DefaultSecondsToExpire, CancellationToken cancellationToken = default);

        /// <summary>
        /// Message time to live into the storage engine before expire. Default 25 hours
        /// </summary>
        const int DefaultSecondsToExpire = 90000;
    }
}
