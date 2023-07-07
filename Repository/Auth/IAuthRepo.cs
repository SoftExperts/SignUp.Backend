using Entities;

namespace Repositories.Auth
{
    public interface IAuthRepo
    {
        Task RegisterUser(User registerUser, string password);
    }
}