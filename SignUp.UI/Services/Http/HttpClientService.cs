using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace SignUp.UI.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory implementation.</param>
        /// <param name="configuration">The configuration implementation.</param>
        public HttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        #region public methods

        /// <summary>
        /// Sends a POST request to the specified path with the provided model as JSON content.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="model">The model to send.</param>
        /// <param name="path">The path of the request.</param>
        /// <returns>The HTTP response message from the request.</returns>
        public async Task<HttpResponseMessage> PostAsync<T>(T model, string path) where T : class
        {
            try
            {
                HttpContent stringContent = SetHttpContent(model);
                var httpClient = await GetHttpClient();
                var result = await httpClient.PostAsync(path, stringContent);

                return new HttpResponseMessage()
                {
                    ReasonPhrase = await result.Content.ReadAsStringAsync(),
                    StatusCode = result.StatusCode,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deserializes the content of the HTTP response message into the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the content into.</typeparam>
        /// <param name="response">The HTTP response message.</param>
        /// <returns>The deserialized object.</returns>
        public async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            string serializedString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(serializedString);
        }

        /// <summary>
        /// Serializes the specified object into a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="model">The object to serialize.</param>
        /// <returns>The JSON string representation of the object.</returns>
        public string Serialize<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        #endregion

        #region private methods

        private async Task<HttpClient> GetHttpClient(bool namedClient = true)
        {
            try
            {
                HttpClient httpClient = namedClient ? httpClientFactory.CreateClient("SignUpAPI") : httpClientFactory.CreateClient();
                return httpClient;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private HttpContent SetHttpContent<T>(T model)
        {
            string serializedString = Serialize(model);
            HttpContent stringContent = new StringContent(serializedString, Encoding.UTF8, MediaTypeNames.Application.Json);
            return stringContent;
        }

        #endregion
    }
}