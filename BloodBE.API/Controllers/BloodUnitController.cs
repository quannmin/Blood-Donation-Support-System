using Microsoft.AspNetCore.Mvc;
using Blood.Contract.Services.Interface;
using Blood.ModelViews.BloodUnitModelViews;
using Blood.Core.APIResponse;
using Blood.Core;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodUnitController : ControllerBase
    {
        private readonly IBloodUnitService _bloodUnitService;

        public BloodUnitController(IBloodUnitService bloodUnitService)
        {
            _bloodUnitService = bloodUnitService;
        }

        /// <summary>
        /// Get all blood units with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBloodUnits(
            [FromQuery] string? bloodGroupName,
            [FromQuery] string? bloodComponent,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _bloodUnitService.GetAllAsync(pageNumber, pageSize, bloodGroupName, bloodComponent);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all blood units without paging
        /// </summary>
        [HttpGet("all/nopaging")]
        public async Task<IActionResult> GetAllWithoutPaging()
        {
            try
            {
                var result = await _bloodUnitService.GetAllWithoutPagingAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a blood unit by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBloodUnitById(int id)
        {
            try
            {
                var result = await _bloodUnitService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new blood unit
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateBloodUnit([FromQuery] CreateBloodUnitModelView model)
        {
            try
            {
                var result = await _bloodUnitService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing blood unit
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBloodUnit(int id, [FromQuery] UpdateBloodUnitModelView model)
        {
            try
            {
                var result = await _bloodUnitService.UpdateAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a blood unit
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBloodUnit(int id)
        {
            try
            {
                var result = await _bloodUnitService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
