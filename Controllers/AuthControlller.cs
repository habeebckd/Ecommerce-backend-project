using E_Commerce.ApiResponse;
using E_Commerce.Dto.user;
using E_Commerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControlller : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public AuthControlller(IAuthServices authServices)
        {
            _authServices = authServices;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRegistrationDto newUser)
        {
            try
            {
                bool isdone = await _authServices.Register(newUser);
                if (!isdone)
                {
                    return BadRequest(new ApiResponse<string>(false, "user already exisit", "[]", null));
                }
                return Ok(new ApiResponse<string>(true, "User Register Sucsessfully", "[done]", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "server error");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto login)
        {
            try
            {
                var res = await _authServices.Login(login);

                if (res.Error == "Not Found")
                {
                    return NotFound("Email is not verified");
                }

                if (res.Error == "invalid password")
                {
                    return BadRequest(res.Error);
                }

                if (res.Error == "User Blocked")
                {
                    return StatusCode(403, "User is blocked by admin!");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("AllUsers")]
        public async Task<IActionResult> AllUsers()
        {
            try
            {
                var users = await _authServices.AllUsers();
                return Ok(new ApiResponse<List<UserListDto>>(true, "user list fetched successfully", users, null));
            }
            catch (Exception ex) 
            {
                return StatusCode(500,new ApiResponse<string>(false,"server error",null,ex.Message));
            }
        }
    }
}

