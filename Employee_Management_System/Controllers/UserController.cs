using Business_Layer.Service;
using Business_Layer.Users;
using Infrastructure.Common.ViewModel.LoginSignup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(SignUpDTO signupDto)
        {
            if(ModelState.IsValid)
            {
                var userSignup=await _userService.SaveUser(signupDto);

                if (userSignup.IsSuccess)
                {
                    return Ok();
                    
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var token =await _userService.UserLogin(loginViewModel);

            if (token == null)
            {
                return Unauthorized("Invalid username or password");
            }
            else if (token.IsSuccess)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
