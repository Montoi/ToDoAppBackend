using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDoApp.Models.Dto;
using ToDoApp.Models;
using AutoMapper;
using ToDoApp.Repository.IRepository;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ToDoApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        protected APIResponse _response;
        
        private IConfiguration _configuration;
        private IUser _dbUser;

        public AuthenticationController(IUser dbRepository, IMapper mapper, IColor color, IConfiguration configuration)
        {
          
            _configuration = configuration;
            this._dbUser = dbRepository;
            this._response = new();

        }

        


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] Authentification login)
        {

            var user = await this._dbUser.GetAsync(u => u.Userr == login.UserName);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (login.UserName != user.Userr)
            {
                return BadRequest("User not found.");
            }

            if (login.PassWord != user.Password)
            {
                return BadRequest("Wrong null.");
            }

            string token = CreateToken(user);


            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Userr),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
