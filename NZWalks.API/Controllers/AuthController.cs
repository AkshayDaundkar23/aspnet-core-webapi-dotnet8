using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            { 
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                // add roles to user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User registerd, Please login");
                    }
                }                
            }

            return BadRequest("Something went wrong");
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (checkPasswordResult)
                {
                    // create token

                    return Ok("User verified");
                }
            }

            return BadRequest("Username or Password incorrect");
        }
    }
}
