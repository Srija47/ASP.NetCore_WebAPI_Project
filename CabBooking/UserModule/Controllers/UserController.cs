using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using UserModule.Models;
using UserModule.Repositories;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;


namespace UserModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _iuserRepository;
        private readonly IConfiguration configuration;
        public UserController(IUserRepository iuserRepository,IConfiguration configuartion)
        {
            _iuserRepository = iuserRepository;
            this.configuration=configuartion;
        }
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> UserRegister(User user)
        {
            return Ok(await _iuserRepository.UserRegister(user));
        }
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>login user details</returns>

        [HttpGet]
        [Route("login/{uname}/{pwd}")]
        public async Task<IActionResult> UserLogin(string uname,string pwd)
        {
            Token token = null;
            try
            {
                var new_user = await _iuserRepository.UserLogin(uname,pwd);
                if (new_user != null)
                {
                    token = new Token() { Id = new_user.Id, token = GenerateJwtToken(uname), message = "Success" };
                }
                else
                {
                    token = new Token() { token = null, message = "UnSuccess" };
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <param name="userid"></param>

        [HttpGet]
        [Route("Profile/{userid}")]
        public async Task<IActionResult> GetProfile(int userid)
        {
            var users = await _iuserRepository.GetProfile(userid);
            if (users == null)
                return Ok("Invalid User");
            else
            {
                return Ok(users);
            }
        }

        /// <summary>
        /// Update User Profile
        /// </summary>

        [HttpPut]
        [Route("EditProfile")]
        public async Task<IActionResult> UpdateProfile(User user)
        {
            return Ok(await _iuserRepository.UpdateProfile(user));
        }

        /// <summary>
        /// Delete user by Userid
        /// </summary>
        [HttpDelete]
        [Route("Delete/{userid}")]
        public async Task<IActionResult> DeleteUser(int userid)
        {
            return Ok(await _iuserRepository.DeleteProfile(userid));
        }
        /*Token based authentication*/
        private string GenerateJwtToken(string uname)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, uname),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, uname),
                new Claim(ClaimTypes.Role,uname)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // recommended is 5 min
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
