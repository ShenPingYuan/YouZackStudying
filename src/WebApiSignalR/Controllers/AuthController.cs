using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiSignalR.Hubs;

namespace WebApiSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHubContext<ChatRoomHub> _hubContext;

        public AuthController(IHubContext<ChatRoomHub> hubContext)
        {
            _hubContext = hubContext;
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
            
            _hubContext.Clients.All.SendAsync("ReceiveOnlineNotice", "MichaelShen");
            return Ok(jwt);
        }
    }
}
