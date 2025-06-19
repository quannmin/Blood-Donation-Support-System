using Microsoft.AspNetCore.Mvc;
using Blood.ModelViews.AuthModelViews;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.ModelViews.AuthModelViews.Request;
using Blood.ModelViews.UserModelViews.Request;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("user-login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel request)
        {
            try
            {
                var result = await _userService.UserLogin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel request)
        {
            try
            {
                var result = await _userService.RegisterUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] UserRegisterRequestModel request)
        {
            try
            {
                var result = await _userService.RegisterDoctor(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("confirm-register")]
        public async Task<IActionResult> ConfirmUserRegister([FromBody] ConfirmUserRegisterRequest request)
        {
            try
            {
                var result = await _userService.ConfirmUserRegister(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromQuery] string email)
        {
            try
            {
                var result = await _userService.ResendOtpAsync(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                var result = await _userService.ForgotPassword(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestModel request)
        {
            try
            {
                var result = await _userService.ResetPassword(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("employee-login")]
        public async Task<IActionResult> EmployeeLogin([FromBody] EmployeeLoginRequestModel request)
        {
            try
            {
                var result = await _userService.EmployeeLogin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("employee-forgot-password")]
        public async Task<IActionResult> EmployeeForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                var result = await _userService.EmployeeForgotPassword(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("employee-reset-password")]
        public async Task<IActionResult> EmployeeResetPassword([FromBody] ResetPasswordRequestModel request)
        {
            try
            {
                var result = await _userService.EmployeeResetPassword(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] NewRefreshTokenRequestModel request)
        {
            try
            {
                var result = await _userService.RefreshToken(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

    }
}
