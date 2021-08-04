using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FoccoEmFrente.Kanban.Api.Configurations;
using FoccoEmFrente.Kanban.Api.Controllers.Attributes;
using FoccoEmFrente.Kanban.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoccoEmFrente.Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModelState]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(ir => ir.Description);
                return BadRequest(string.Join("; ", errors));
            }

            return Ok(await GetFullJwt(user.Email));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                var fullJwt = await GetFullJwt(loginUser.Email);
                return Ok(fullJwt);
            }

            if (result.IsLockedOut)
            {
                return BadRequest("This user is temporarily blocked");
            }

            return BadRequest("Incorrect user or password");
        }

        private async Task<string> GetFullJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var tokenDescription = GetSecurityTokenDescriptor(user);
            return GenerateToken(tokenDescription);
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(IdentityUser user)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var claimsIndenty = new ClaimsIdentity();
            claimsIndenty.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, user.Id));
            claimsIndenty.AddClaim(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddMonths(12),
                Subject = claimsIndenty
            };

            return tokenDescription;
        }

        private string GenerateToken(SecurityTokenDescriptor tokenDescription)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            return tokenHandle.WriteToken(tokenHandle.CreateToken(tokenDescription));
        }
    }
}
