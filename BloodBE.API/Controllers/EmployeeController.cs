using Microsoft.AspNetCore.Mvc;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.Core.Base;
using Blood.ModelViews.UserModelViews.Request;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUserService _userService;

        public EmployeeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployee([FromForm] CreateEmployeeRequest request)
        {
            try
            {
                var result = await _userService.CreateEmployee(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPut("update-doctor-profile")]
        public async Task<IActionResult> UpdateDoctorProfile([FromForm] UpdateEmployeeProfileRequest request)
        {
            try
            {
                var result = await _userService.UpdateDoctorProfile(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateEmployeeStatus([FromBody] UpdateUserStatusRequest request)
        {
            try
            {
                var result = await _userService.UpdateEmployeeStatus(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteEmployee([FromBody] DeleteUserRequest request)
        {
            try
            {
                var result = await _userService.DeleteEmployee(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpPost("get-doctors-pagination")]
        public async Task<IActionResult> GetDoctorPagination([FromQuery] string? Email, [FromQuery] int? PageIndex = 1, [FromQuery] int? PageSize = 10)
        {
            try
            {
                var result = await _userService.GetDoctorPagination(Email, PageIndex, PageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("get-all-doctors")]
        public async Task<IActionResult> GetAllDoctor()
        {
            try
            {
                var result = await _userService.GetAllDoctor();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var result = await _userService.GetAllEmployee();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var result = await _userService.GetEmployeeById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
