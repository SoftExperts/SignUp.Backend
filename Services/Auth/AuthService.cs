using DTOs;
using Entities;
using Repositories.Auth;
using System.Net;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="authRepo">The authentication repository implementation.</param>
        public AuthService(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        #region Public Methods Region

        /// <summary>
        /// Registers a new user using the Auth Repository.
        /// </summary>
        /// <param name="userDto">The user data transfer object containing registration information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RegisterUser(UserDto userDto)
        {
            try
            {
                var user = await ToEntity(userDto);

                await _authRepo.RegisterUser(user, userDto.Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Private Methods Region

        /// <summary>
        /// Converts a UserDto object to a User entity object.
        /// </summary>
        /// <param name="registerUserDto">The UserDto object to convert.</param>
        /// <returns>The converted User entity object.</returns>
        public async Task<User> ToEntity(UserDto registerUserDto)
        {
            try
            {
                if (registerUserDto == null) throw new ArgumentNullException(nameof(registerUserDto));

                return new User()
                {
                    FirstName = registerUserDto.FirstName,
                    LastName = registerUserDto.LastName,
                    Email = registerUserDto.Email,
                    UserName = registerUserDto.Email,
                    Password = registerUserDto.Password,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

}
