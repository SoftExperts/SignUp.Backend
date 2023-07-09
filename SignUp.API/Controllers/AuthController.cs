using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;

namespace SignUp.Backend.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{v:apiVersion}/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service implementation.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userDto">The user data transfer object containing registration information.</param>
        /// <returns>An IActionResult representing the result of the registration operation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDto userDto)
        {
            try
            {
                await _authService.RegisterUser(userDto);
                return Ok("Successfully User Registered!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
