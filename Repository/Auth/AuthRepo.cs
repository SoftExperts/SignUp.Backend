using Entities;
using Microsoft.AspNetCore.Identity;

namespace Repositories.Auth
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthRepo"/> class.
        /// </summary>
        /// <param name="userManager">The user manager implementation.</param>
        public AuthRepo(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUser">The user object to register.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RegisterUser(User registerUser, string password)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(registerUser.Email);

                if (userExist != null)
                    throw new Exception($"User {registerUser.Email} already exists.");

                var result = await _userManager.CreateAsync(registerUser, password);
                if (!result.Succeeded)
                    throw new Exception($"Error: {result.Errors.FirstOrDefault()?.Description}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}