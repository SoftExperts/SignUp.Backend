using SignUp.UI.Models;
using System.Net.Http;

namespace SignUp.UI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientService _clientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="clientService">The HttpClient service implementation.</param>
        public AuthService(IHttpClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user object containing registration data.</param>
        /// <returns>The HTTP response message from the registration request.</returns>
        public async Task<HttpResponseMessage> RegisterUser(User user)
        {
            try
            {
                return await _clientService.PostAsync<User>(user, "Auth/register");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
