using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.MessageStorage
{
    public interface IMessageStorage
    {
        Task<string> InsertAsync(string message, CancellationToken cancellationToken = default);
    }
}
