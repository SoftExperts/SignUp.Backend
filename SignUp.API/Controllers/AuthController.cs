﻿using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;

namespace SignUp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
              _authService = authService;
        }

        [HttpPost]
        public async Task RegisterUser(UserDto userDto)
        {
            try
            {
                await _authService.RegisterUser(userDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}