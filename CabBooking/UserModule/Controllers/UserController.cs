using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserModule.Models;
using UserModule.Repositories;

namespace UserModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _iuserRepository;
        public UserController(IUserRepository iuserRepository)
        {
            _iuserRepository = iuserRepository;
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
        [Route("login")]
        public async Task<IActionResult> UserLogin(Login login)
        {
            var userlogin = await _iuserRepository.UserLogin(login);
            return Ok(userlogin);
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
    }
}
