using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplicationIdentity.Entities;

namespace WebApplicationIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthController(RoleManager<Role> roleManager, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost("initAdmin")]
        public async Task<IActionResult> CreateAdminAccount()
        {
            bool roleAdminExists = await _roleManager.RoleExistsAsync("admin");
            if (!roleAdminExists)
            {
                Role? roleAdmin = new Role { Name = "admin" };
                IdentityResult? roleCreatedResult = await _roleManager.CreateAsync(roleAdmin);
                if (!roleCreatedResult.Succeeded)
                {
                    return Problem(string.Join(';', roleCreatedResult.Errors));
                }
            }
            User? userAdmin = await _userManager.FindByNameAsync("admin");
            if (userAdmin == null)
            {
                userAdmin = new User { UserName = "admin", Email = "2439739932@qq.com" };
                var userCreatedResult = await _userManager.CreateAsync(userAdmin, "2439739932");
                if (!userCreatedResult.Succeeded)
                {
                    return Problem(string.Join(';', userCreatedResult.Errors));
                }
            }
            if (await _userManager.IsInRoleAsync(userAdmin, "admin"))
            {
                return Ok("账户初始化成功");
            }
            var addToRoleResult = await _userManager.AddToRoleAsync(userAdmin, "admin");
            if (!addToRoleResult.Succeeded)
            {
                return Problem(string.Join(';', addToRoleResult.Errors));
            }
            return Ok("账户初始化成功");
        }
        [HttpPost("sendToken")]
        public async Task<IActionResult> SendToken()
        {
            string email = "2439739932@qq.com";
            var user = await _userManager.FindByEmailAsync(email);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine("想邮箱发送邮件");
            return Ok(token);
        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string userName, string token)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.ResetPasswordAsync(user, token, "123456");
            await _userManager.ResetAccessFailedCountAsync(user);
            return Ok();
        }
        [HttpPost("getJwtToken")]
        public IActionResult GenerateJwtToken()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
            claims.Add(new Claim(ClaimTypes.Name, "MichaelShen"));
            claims.Add(new Claim(ClaimTypes.Role, "admin"));
            string key = "kfsl;dakgjkas;ldfjERSadfkaT#RA%dfasld";
            DateTime expires = DateTime.Now.AddMinutes(2);
            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                claims: claims, expires: expires, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return Ok(jwt);
        }
        [HttpGet("varifyJwtToken")]
        public IActionResult VarifyJwtToken(string token)
        {
            string key = "kfsl;dakgjkas;ldfjERSadfkaT#RA%dfasld";
            //string key = "kfsl;dakgjkas;ldfjERSadfkaT#RA%dfasld";
            JwtSecurityTokenHandler tokenHandler = new();
            TokenValidationParameters validationParameters = new();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            validationParameters.IssuerSigningKey = securityKey;
            validationParameters.ValidateIssuer = false;
            validationParameters.ValidateAudience = false;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken security);
            List<string> result=new List<string>();
            foreach (var claim in principal.Claims)
            {
                result.Add($"{claim.Type}={ claim.Value}");
            }
            return Ok(result);
        }
        [Authorize]
        [HttpGet("userInfo")]
        public IActionResult GetUserInfo()
        {
            return Ok(User.Claims.Select(c => c.Type + ":" + c.Value));
        }
    }
}
