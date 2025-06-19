using Microsoft.AspNetCore.Mvc;
using Blood.Contract.Services.Interface;
using Blood.ModelViews.BloodCompatibilityModelViews;
using Blood.Core;
using Blood.Core.APIResponse;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodCompatibilityController : ControllerBase
    {
        private readonly IBloodCompatibilityService _bloodCompatibilityService;

        public BloodCompatibilityController(IBloodCompatibilityService bloodCompatibilityService)
        {
            _bloodCompatibilityService = bloodCompatibilityService;
        }

        /// <summary>
        /// Get all blood compatibilities with optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<BasePaginatedList<BloodCompatibilityModelView>>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? bloodComponent = null,
            [FromQuery] string? donorBloodGroupName = null,
            [FromQuery] string? recipientBloodGroupName = null)
        {
            try
            {
                var result = await _bloodCompatibilityService.GetAllAsync(pageNumber, pageSize, bloodComponent, donorBloodGroupName, recipientBloodGroupName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a blood compatibility by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<BloodCompatibilityModelView>>> GetById(int id)
        {
            try
            {
                var result = await _bloodCompatibilityService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new blood compatibility
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CreateBloodCompatibilityModelView model)
        {
            try
            {
                var result = await _bloodCompatibilityService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing blood compatibility
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Update(int id, [FromBody] UpdateBloodCompatibilityModelView model)
        {
            try
            {
                var result = await _bloodCompatibilityService.UpdateAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a blood compatibility
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResult<object>>> Delete(int id)
        {
            try
            {
                var result = await _bloodCompatibilityService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
