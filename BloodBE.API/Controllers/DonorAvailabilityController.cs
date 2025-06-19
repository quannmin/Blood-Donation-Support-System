using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.ModelViews.DonorAvailabilityModelViews;
using Microsoft.AspNetCore.Mvc;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorAvailabilityController : ControllerBase
    {
        private readonly IDonorAvailabilityService _donorAvailabilityService;

        public DonorAvailabilityController(IDonorAvailabilityService donorAvailabilityService)
        {
            _donorAvailabilityService = donorAvailabilityService;
        }

        /// <summary>
        /// Get all donor availability records with pagination and optional name filter
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPaging(
            [FromQuery] string? userName,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                var result = await _donorAvailabilityService.GetAllAsync(pageNumber, pageSize, userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all donor availability records without pagination
        /// </summary>
        [HttpGet("all/nopaging")]
        public async Task<IActionResult> GetAllWithoutPaging()
        {
            try
            {
                var result = await _donorAvailabilityService.GetAllWithoutPagingAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get donor availability by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _donorAvailabilityService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create new donor availability
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateDonorAvailabilityModelView model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _donorAvailabilityService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update donor availability
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateDonorAvailabilityModelView model)
        {
            try
            {
                var result = await _donorAvailabilityService.UpdateAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete donor availability
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _donorAvailabilityService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
