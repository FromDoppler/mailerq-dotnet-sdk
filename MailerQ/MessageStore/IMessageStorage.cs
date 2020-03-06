using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.MessageStore
{
    public interface IMessageStorage
    {
        Task<string> InsertAsync(string message, int secondsToExpire = DefaultSecondsToExpire, CancellationToken cancellationToken = default);

        /// <summary>
        /// Message time to live into the storage engine before expire. Default 25 hours
        /// </summary>
        const int DefaultSecondsToExpire = 90000;
    }
}
