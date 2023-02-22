using e_Commerce.Data.Entities;
using e_Commerce.Services.Dtos;
using e_Commerce.Services.Interfaces;
using e_Commerce.Services.Requests;
using FootballApp.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;

namespace e_Commerce_Website.Controllers
{
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;


        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterUserRequest createUserRequest)
        {
            
            var user = await _userService.Regiser(createUserRequest);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDto userDto)
        {
            var token = await _userService.Login(userDto);

            return Ok(token);
        }

        [HttpGet("GetCart")]
        public async Task<ActionResult> GetShoppingCart()
        {
            var userId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
            var shoppingCart = _userService.ShowCart(userId);

            return Ok(shoppingCart);
        }
    }
}
