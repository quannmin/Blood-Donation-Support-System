using Microsoft.AspNetCore.Mvc;
using Blood.Contract.Services.Interface;
using Blood.ModelViews.DonationModelViews;
using Blood.Core.APIResponse;
using Blood.Core;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        /// <summary>
        /// Get all donations with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllDonations(
            [FromQuery] int? userId,
            [FromQuery] int? bloodRequestId,
            [FromQuery] DateTime? donationDate,
            int pageNumber = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _donationService.GetAllAsync(pageNumber, pageSize, userId, bloodRequestId, donationDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all donations without paging
        /// </summary>
        [HttpGet("all/nopaging")]
        public async Task<IActionResult> GetAllDonationsNoPaging()
        {
            try
            {
                var result = await _donationService.GetAllWithoutPagingAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a donation by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDonationById(int id)
        {
            try
            {
                var result = await _donationService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new donation
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateDonation([FromQuery] CreateDonationModelView model)
        {
            try
            {
                var result = await _donationService.CreateAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update an existing donation
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDonation(int id, [FromQuery] UpdateDonationModelView model)
        {
            try
            {
                var result = await _donationService.UpdateAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a donation (soft delete)
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            try
            {
                var result = await _donationService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
