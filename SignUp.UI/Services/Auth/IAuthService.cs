using SignUp.UI.Models;

namespace SignUp.UI.Services.Auth
{
    public interface IAuthService
    {
        Task<HttpResponseMessage> RegisterUser(User userDto);
    }
}
