using EasyNetQ;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MailerQ.ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly ApiClientSettings _apiClientSettings;
        private UriBuilder _apiUri;
        private readonly ILogger<ApiClient> _logger;

        private const string EndpointError = "errors";
        private const string EndpointPause = "pauses";
        private const string EndpointInject = "inject";

        private const string MethodPost = WebRequestMethods.Http.Post;
        private const string MethodDelete = "DELETE";
        private const string MethodGet = WebRequestMethods.Http.Get;

        public ApiClient(ApiClientSettings apiClientSettings, ILogger<ApiClient> logger)
        {
            _apiClientSettings = apiClientSettings;
            _apiUri = new UriBuilder(_apiClientSettings.Url)
            {
                Path = _apiClientSettings.Version
            };
            _logger = logger;
        }

        public ApiClient(IOptions<ApiClientSettings> options, ILogger<ApiClient> logger) : this(options.Value, logger) { }

        public bool Post(Error error)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(error);
                var result = MailerQApiClient(EndpointError, MethodPost, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    EndpointError,
                    MethodPost);
                throw exception;
            }
        }

        public bool Post(Pause pause)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(pause);
                var result = MailerQApiClient(EndpointPause, MethodPost, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    EndpointPause,
                    MethodPost);
                throw exception;
            }
        }

        public bool Post(Inject inject)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(inject);
                var result = MailerQApiClient(EndpointInject, MethodPost, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    EndpointInject,
                    MethodPost);
                throw exception;
            }
        }
        public bool Delete(Error error)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(error);
                var result = MailerQApiClient(EndpointError, MethodDelete, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    EndpointError,
                    MethodDelete);
                throw exception;
            }
        }
        public bool Delete(Pause pause)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(pause);
                var result = MailerQApiClient(EndpointPause, MethodDelete, jsonContent);
                return result == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    EndpointPause,
                    MethodDelete);
                throw exception;
            }
        }

        private HttpStatusCode MailerQApiClient(string endpoint, string method, string content)
        {
            _apiUri.Path = Path.Combine(_apiUri.Path, endpoint);
            var request = WebRequest.CreateHttp(_apiUri.Uri);

            request.Method = method;
            request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("Authorization", "Bearer " + _apiClientSettings.Token);

            using (var requestStream = request.GetRequestStream())
            {
                using var streamWriter = new StreamWriter(requestStream);
                streamWriter.Write(content);
            }
            var response = (HttpWebResponse)request.GetResponse();
            return response.StatusCode;
        }

        public List<Error> Get(Error error)
        {
            try
            {
                var result = MailerQApiClientGet(EndpointError, MethodGet);
                return JsonConvert.DeserializeObject<List<Error>>(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}. Exception message: {Message}",
                    EndpointError,
                    MethodGet,
                    exception.Message);
                throw exception;
            }
        }

        public List<Pause> Get(Pause pause)
        {
            try
            {
                var result = MailerQApiClientGet(EndpointPause, MethodGet);
                return JsonConvert.DeserializeObject<List<Pause>>(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                    "Endpoint: {Endpoint}. Method: {Method}.",
                    EndpointPause,
                    MethodGet);
                throw exception;
            }
        }

        private string MailerQApiClientGet(string endpoint, string method)
        {
            _apiUri.Path = Path.Combine(_apiUri.Path, endpoint);
            var request = (HttpWebRequest)WebRequest.Create(_apiUri.Uri);

            request.Method = method;
            request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("Authorization", "Bearer " + _apiClientSettings.Token);

            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using var stream = response.GetResponseStream();
                using var streamReader = new StreamReader(stream);
                content = streamReader.ReadToEnd();
            }
            return content;
        }
    }
}
