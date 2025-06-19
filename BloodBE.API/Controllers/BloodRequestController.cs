using Microsoft.AspNetCore.Mvc;
using Blood.Contract.Services.Interface;
using Blood.ModelViews.BloodRequestModelViews;
using Blood.Core.APIResponse;
using Blood.Core;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestController : ControllerBase
    {
        private readonly IBloodRequestService _bloodRequestService;

        public BloodRequestController(IBloodRequestService bloodRequestService)
        {
            _bloodRequestService = bloodRequestService;
        }

        /// <summary>
        /// Get all blood requests with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBloodRequests(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? requestSource = null,
            [FromQuery] DateTime? requestDate = null,
            [FromQuery] string? status = null,
            [FromQuery] string? bloodComponent = null,
            [FromQuery] int? requestId = null,
            [FromQuery] string? fullName = null)
        {
            try
            {
                var result = await _bloodRequestService.GetAllAsync(
                    pageNumber, pageSize, requestSource, requestDate,
                    status, bloodComponent, requestId, fullName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all blood requests without paging
        /// </summary>
        [HttpGet("all/nopaging")]
        public async Task<IActionResult> GetAllWithoutPaging()
        {
            try
            {
                var result = await _bloodRequestService.GetAllWithoutPagingAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a blood request by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBloodRequestById(int id)
        {
            try
            {
                var result = await _bloodRequestService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new blood request
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateBloodRequest([FromForm] CreateBloodRequestModelView model)
        {
            try
            {
                var result = await _bloodRequestService.CreateAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing blood request
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBloodRequest(int id, [FromForm] UpdateBloodRequestModelView model)
        {
            try
            {
                var result = await _bloodRequestService.UpdateAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a blood request
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBloodRequest(int id)
        {
            try
            {
                var result = await _bloodRequestService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
