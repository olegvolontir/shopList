using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopList.Models.Requests;
using ShopList.Services;

namespace ShopList.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ObjectResult> Register([FromBody] UserRegisterRequest userRequest,
            [FromQuery] string role)
        {
            return Ok(await _userService.RegisterUser(userRequest, role));
        }

        [HttpPost("Login")]
        public async Task<ObjectResult> Login([FromBody] UserLoginRequest userRequest)
        {
            return Ok(await _userService.Login(userRequest.Username, userRequest.Password));
        }

        [HttpPut("Token/Refresh")]
        public async Task<ObjectResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            return Ok(await _userService.RefreshToken(refreshTokenRequest.RefreshToken));
        }

        [HttpPut("Token/Revoke")]
        public async Task<ObjectResult> RevokeToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            return Ok(await _userService.RevokeRefreshToken(refreshTokenRequest.RefreshToken));
        }

    }
}
