using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UsersIO.Api.Configuration;
using UsersIO.Api.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersIO.Api.Controllers
{
    [Route("api/Authentication")]
    public class AuthenticationController : ControllerBase
    {

        private readonly SignInManager<IdentityUser> _signManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SecurityConfig _securityConfig;

        public AuthenticationController(SignInManager<IdentityUser> signManager, UserManager<IdentityUser> userManager, IOptions<SecurityConfig> securityConfig)
        {
            _signManager = signManager;
            _userManager = userManager;
            _securityConfig = securityConfig.Value;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Registrar([FromBody] RegisterUserDTO registerUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _signManager.SignInAsync(user, false);

            return Ok(await GenerateJwt(registerUser.Email));

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return Ok(await GenerateJwt(loginUser.Email));
            }

            return BadRequest("Usuário ou senha inválidos!");

        }

        private async Task<string> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_securityConfig.Secret);

            var token = new SecurityTokenDescriptor
            {
                Issuer = _securityConfig.Emitter,
                Audience = _securityConfig.Validation,
                Expires = DateTime.UtcNow.AddHours(_securityConfig.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(token));
        }

    }
}
