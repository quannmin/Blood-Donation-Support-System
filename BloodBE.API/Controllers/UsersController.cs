using Microsoft.AspNetCore.Mvc;
using Blood.Contract.Services.Interface;
using Blood.Core.Base;
using Blood.ModelViews.UserModelViews;
using Blood.Core.APIResponse;
using Blood.ModelViews.UserModelViews.Request;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("update-user-profile")]
        public async Task<IActionResult> UpdateUserAndOwnerProfile([FromForm] UpdateUserProfileRequest request)
        {
            try
            {
                var result = await _userService.UpdateUserProfile(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("get-pagination")]
        public async Task<IActionResult> GetUserPagination([FromQuery] string? Email, [FromQuery] int? PageIndex = 1, [FromQuery] int? PageSize = 10)
        {
            try
            {
                var result = await _userService.GetUserPagination(Email, PageIndex, PageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var result = await _userService.GetUserById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request)
        {
            try
            {
                var result = await _userService.DeleteUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UpdateUserStatusRequest request)
        {
            try
            {
                var result = await _userService.UpdateUserStatus(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var result = await _userService.GetAllUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            try
            {
                var result = await _userService.UploadImage(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
