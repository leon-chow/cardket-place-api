using cardket_place_api.Models.DTOs;
using cardket_place_api.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cardket_place_api.Services
{
    public class UserService
    {
        private IConfiguration _configuration;
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly AuthService _authService;
        public UserService(IConfiguration configuration, SignInManager<Account> signInManager, UserManager<Account> userManager, AuthService authService) 
        { 
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<(bool IsAuthenticated, JwtSecurityToken Token, string Message)> HandleLogin(LoginDto model)
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
                    return (true, token, "Success!");
                }
            }
            return (false, null, "Invalid login attempt.");
        }

        public async Task<(bool hasSucceeded, IEnumerable<IdentityError>? errors)> HandleRegister(RegisterDto model)
        {
            // Check if the username/email is available
            // check that the credentials are good enough
            // Register
            var user = new Account { UserName = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return (true, null);
            }
            return (false, result.Errors);
        } 

        public async Task<(bool hasSucceeded, IEnumerable<IdentityError>? errors)> HandleUpdateUser(UserDto updatedUser)
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
                    return (false, result.Errors);
                }

                return (true, null);
            }
            return (false, null);
        }
    }
}
