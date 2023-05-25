using Assignment3API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment3API.Service;

namespace Assignment3API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;
        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO registerDTO)
        {
            return await _userService.Register(registerDTO.Username, registerDTO.Password);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDTO)
        {
            var result = await _userService.Login(loginDTO.Username, loginDTO.Password);

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }

   
}
