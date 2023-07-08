using DTOs;
using Entities;
using Repositories.Auth;
using System.Net;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        
        public AuthService(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        #region Public Methods Region
        
        /// <summary>
        /// This method is used to register user by using Auth Repository.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
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
