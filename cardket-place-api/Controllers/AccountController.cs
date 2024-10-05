using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cardket_place_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) { }
        
        [HttpPost("login")]
        public void login()
        {
            Console.WriteLine("Logged in!");
        }

        [HttpGet("logout")]
        public void logout() { 
        
        }

        [HttpPost("register")]
        public void register()
        {
            // Check if the username/email is available
            // check that the credentials are good enough
            // Register
        }
    }
}
