using Entities;
using Microsoft.AspNetCore.Identity;

namespace Repositories.Auth
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<User> _userManager;

        public AuthRepo(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task RegisterUser(User registerUser, string password)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(registerUser.Email);

                if (userExist != null)
                    throw new Exception($"User {registerUser.Email} is already Exist.");
                
                await _userManager.CreateAsync(registerUser, password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}