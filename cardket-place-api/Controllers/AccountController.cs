using cardket_place_api.Models.DTOs;
using cardket_place_api.Models.Entities;
using cardket_place_api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cardket_place_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly AuthService _authService;
        private readonly UserService _userService;
        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager, IConfiguration config, ILogger<AccountController> logger, AuthService authService, UserService userService) {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _authService = authService;
            _userService = userService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model) 
        {
            if (ModelState.IsValid)
            {
                var (IsAuthenticated, Token, Message) = await _userService.HandleLogin(model);
                if (IsAuthenticated)
                {
                    return Ok(new { token = Token.EncodedPayload });
                }
                return Unauthorized(new { message = "Invalid login attempt. " });
            }
            return BadRequest("Invalid login request.");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout() { 
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Successfully logged out!" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            // Check if the username/email is available
            // check that the credentials are good enough
            // Register
            if (ModelState.IsValid)
            {
                var user = new Account { UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok(new { message = "User registered successfully!" });
                }
                return BadRequest(result.Errors);
            }
            return BadRequest("Invalid model.");
        }
        [HttpPost("update/{username}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser)
        {
            var user = await _userManager.FindByNameAsync(updatedUser.Username);
            if (user != null)
            {
                // consider using mapper here
                user.ImageUrl = updatedUser.ImageUrl;
                user.Nickname = updatedUser.Nickname;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(user);
            }
            return NotFound();
        }
    }
}
