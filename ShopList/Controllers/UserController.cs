using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopList.Models.Requests;
using ShopList.Models.Responses;
using ShopList.Services;
using ShopList.Models.Constants;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ShopList.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ObjectResult> Register([FromBody] UserRegisterRequest userRequest,
            [FromQuery] string role)
        {
            var result = await _userService.RegisterUser(userRequest, role);
            if (result ==null)
            {
                return BadRequest("Invalid typed role!");
            }
            return Ok(result);
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

        [Authorize(Roles ="Administrator")]
        [HttpGet]
        public async Task<ObjectResult> GetUsers()
        {
            var users = await _userService.GetUsers();

            return Ok(_mapper.Map<List<GetUserResponse>>(users));
        }
    }
}
