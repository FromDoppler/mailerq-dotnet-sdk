using System;
using System.Collections.Generic;
using System.Text;

namespace MailerQ.ApiClient
{
    public interface IApiClient
    {
        bool Post(Error error);
        bool Post(Pause pause);
        bool Post(Inject inject);
        bool Delete(Error error);
        bool Delete(Pause pause);
        List<Error> Get(Error error);
        List<Pause> Get(Pause pause);
    }
}
