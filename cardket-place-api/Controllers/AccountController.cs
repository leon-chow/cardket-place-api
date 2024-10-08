using cardket_place_api.Models.DTOs;
using cardket_place_api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager, IConfiguration config, ILogger<AccountController> logger) {
            this._config = config;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model) 
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Login Successful!" });
                }
                return Unauthorized(new { message = "Invalid login attempt. " });
            }
            return BadRequest("Invalid login request.");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> logout() { 
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Successfully logged out!" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            // Check if the username/email is available
            // check that the credentials are good enough
            // Register
            if (!ModelState.IsValid)
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
    }
}
