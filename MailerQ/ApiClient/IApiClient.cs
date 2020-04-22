using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailerQ.ApiClient
{
    public interface IApiClient
    {
        Task<bool> Post(Error error);
        Task<bool> Post(Pause pause);
        Task<bool> Post(Inject inject);
        Task<bool> Delete(Error error);
        Task<bool> Delete(Pause pause);
        ICollection<Error> Get(Error error);
        ICollection<Pause> Get(Pause pause);
    }
}
