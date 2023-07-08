using DTOs;

namespace Services.Auth
{
    public interface IAuthService
    {
        Task RegisterUser(UserDto userDto);
    }
}
