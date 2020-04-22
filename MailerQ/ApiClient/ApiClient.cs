using EasyNetQ;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MailerQ.ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly ApiClientSettings _apiClientSettings;
        private UriBuilder _apiUri;
        private readonly ILogger<ApiClient> _logger;
        private readonly IHttpClientFactory _httpFactory;

        public ApiClient(
            ILogger<ApiClient> logger,
            ApiClientSettings apiClientSettings,
            IHttpClientFactory httpFactory)
        {
            _apiClientSettings = apiClientSettings;
            _apiUri = new UriBuilder(_apiClientSettings.Url)
            {
                Path = _apiClientSettings.Version
            };
            _logger = logger;
            _httpFactory = httpFactory;
        }

        public ApiClient(
            ILogger<ApiClient> logger,
            IOptions<ApiClientSettings> options,
            IHttpClientFactory httpFactory) : this(logger, options.Value, httpFactory) { }

        public async Task<bool> Post(Error error)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(error);
                var result = await MailerQApiClientPostAsync(error.Endpoint, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    error.Endpoint,
                    nameof(Post));
                throw exception;
            }
        }

        public async Task<bool> Post(Pause pause)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(pause);
                var result = await MailerQApiClientPostAsync(pause.Endpoint, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    pause.Endpoint,
                    nameof(Post));
                throw exception;
            }
        }

        public async Task<bool> Post(Inject inject)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(inject);
                var result = await MailerQApiClientPostAsync(inject.Endpoint, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    inject.Endpoint,
                    nameof(Post));
                throw exception;
            }
        }

        private async Task<HttpStatusCode> MailerQApiClientPostAsync(string endpoint, string content)
        {
            _apiUri.Path = Path.Combine(_apiUri.Path, endpoint);

            var client = _httpFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiClientSettings.Token);

            HttpContent httpContent = new StringContent(content);
            httpContent.Headers.ContentType.MediaType = "application/json";

            var httpResponse = await client.PostAsync(_apiUri.Uri, httpContent);

            return httpResponse.StatusCode;
        }

        public async Task<bool> Delete(Error error)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(error);
                var result = await MailerQApiClientDeleteAsync(error.Endpoint, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    error.Endpoint,
                    nameof(Delete));
                throw exception;
            }
        }
        public async Task<bool> Delete(Pause pause)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(pause);
                var result = await MailerQApiClientDeleteAsync(pause.Endpoint, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    pause.Endpoint,
                    nameof(Delete));
                throw exception;
            }
        }

        private async Task<HttpStatusCode> MailerQApiClientDeleteAsync(string endpoint, string content)
        {
            _apiUri.Path = Path.Combine(_apiUri.Path, endpoint);

            var client = _httpFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiClientSettings.Token);

            HttpContent httpContent = new StringContent(content);
            httpContent.Headers.ContentType.MediaType = "application/json";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, _apiUri.Uri)
            {
                Content = httpContent
            };

            var httpResponse = await client.SendAsync(httpRequestMessage);

            return httpResponse.StatusCode;
        }

        public ICollection<Error> Get(Error error)
        {
            try
            {
                var result = MailerQApiClientGetAsync(error.Endpoint);
                return JsonConvert.DeserializeObject<List<Error>>(result.Result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    error.Endpoint,
                    nameof(Get));
                throw exception;
            }
        }

        public ICollection<Pause> Get(Pause pause)
        {
            try
            {
                var result = MailerQApiClientGetAsync(pause.Endpoint);
                return JsonConvert.DeserializeObject<List<Pause>>(result.Result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    pause.Endpoint,
                    nameof(Get));
                throw exception;
            }
        }

        private async Task<string> MailerQApiClientGetAsync(string endpoint)
        {
            _apiUri.Path = Path.Combine(_apiUri.Path, endpoint);

            var client = _httpFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiClientSettings.Token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var httpResponse = await client.GetAsync(_apiUri.Uri);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }

            return null;
        }
    }
}
