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
        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager, IConfiguration config, ILogger<AccountController> logger, AuthService authService) {
            this._config = config;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._authService = authService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model) 
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    if (user != null)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };
                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }
                        var token = _authService.GenerateJWTToken(authClaims);

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
                    }
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
