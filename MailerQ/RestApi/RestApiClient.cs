using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MailerQ.RestApi
{
    /// <summary>
    /// A client of MailerQ Rest API implement with HttpClient
    /// </summary>
    public class RestApiClient : IRestApiClient
    {
        private readonly MailerQConfiguration _settings;
        private readonly IHttpClientFactory _httpFactory;

        /// <summary>
        /// Initializes a new instance of MailerQ Rest API Client
        /// </summary>
        /// <param name="options">MailerQ settings with API Url and Authorization Token</param>
        /// <param name="httpFactory">HttpClienFactory for create HttpClient objects</param>
        public RestApiClient(
            IOptions<MailerQConfiguration> options,
            IHttpClientFactory httpFactory)
        {
            _settings = options.Value;
            _httpFactory = httpFactory;
        }

        private HttpClient CreateHttpClient()
        {
            var client = _httpFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.RestApiToken);
            return client;
        }

        private string GetEndPoint(Type type)
        {
            var typeName = type.Name;

            // HACK: this allow get the correct endpoint for obsolete classes that inhiret from the classes with expected name
            if (type.BaseType != null && typeof(IRestApiModel).IsAssignableFrom(type.BaseType))
            {
                typeName = type.BaseType.Name;
            }

            var endpointName = $"{typeName.ToLowerInvariant()}s";

            if (type == typeof(Inject) || type == typeof(OutgoingMessage))
            {
                endpointName = "inject";
            }

            var baseUri = new Uri(_settings.RestApiUrl);
            var endpointUri = new Uri(baseUri, endpointName);
            return endpointUri.ToString();
        }

        /// <inheritdoc />
        public async Task<IRestApiResponse<T>> GetAsync<T>(
            IRestApiRequest<T> restApiRequest = default,
            CancellationToken cancellationToken = default) where T : IRestApiModel
        {
            try
            {
                var endpoint = GetEndPoint(typeof(T));
                if (!string.IsNullOrWhiteSpace(restApiRequest?.QueryString))
                {
                    endpoint += $"?{restApiRequest.QueryString}";
                }

                var client = CreateHttpClient();
                var httpResponse = await client.GetAsync(endpoint, cancellationToken);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<List<T>>(result);
                    return new RestApiResponse<T>(httpResponse.StatusCode, content);
                }

                return new RestApiResponse<T>(httpResponse.StatusCode);
            }
            catch (Exception exception)
            {
                throw new RestApiException("Unexpected exception", exception);
            }
        }

        /// <inheritdoc />
        public async Task<IRestApiResponse<T>> DeleteAsync<T>(T model, CancellationToken cancellationToken = default) where T : IRestApiModel
        {
            try
            {
                var endpoint = GetEndPoint(model.GetType());
                var jsonContent = JsonConvert.SerializeObject(model);

                var client = CreateHttpClient();
                HttpContent httpContent = new StringContent(jsonContent);
                httpContent.Headers.ContentType.MediaType = "application/json";

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, endpoint)
                {
                    Content = httpContent
                };

                var httpResponse = await client.SendAsync(httpRequestMessage, cancellationToken);

                return new RestApiResponse<T>(httpResponse.StatusCode);
            }
            catch (Exception exception)
            {
                throw new RestApiException("Unexpected exception", exception);
            }
        }

        /// <inheritdoc />
        public async Task<IRestApiResponse<T>> PostAsync<T>(T model, CancellationToken cancellationToken = default) where T : IRestApiModel
        {
            try
            {
                var endpoint = GetEndPoint(model.GetType());
                var jsonContent = JsonConvert.SerializeObject(model);

                var client = CreateHttpClient();
                HttpContent httpContent = new StringContent(jsonContent);
                httpContent.Headers.ContentType.MediaType = "application/json";
                var httpResponse = await client.PostAsync(endpoint, httpContent, cancellationToken);

                return new RestApiResponse<T>(httpResponse.StatusCode);
            }
            catch (Exception exception)
            {
                throw new RestApiException("Unexpected exception", exception);
            }
        }
    }
}
