using Microsoft.AspNetCore.Mvc;
using Blood.Contract.Services.Interface;
using Blood.Core.Base;
using Blood.Core;
using Blood.ModelViews.DonorProfileViews;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorProfilesController : ControllerBase
    {
        private readonly IDonorProfileService _donorProfileService;

        public DonorProfilesController(IDonorProfileService donorProfileService)
        {
            _donorProfileService = donorProfileService;
        }

        /// <summary>
        /// Get all donor profiles with pagination and filters
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <param name="id">Filter by donor profile ID</param>
        /// <param name="userId">Filter by user ID</param>
        /// <param name="bloodTypeId">Filter by blood type ID</param>
        /// <param name="healthStatus">Filter by health status</param>
        /// <param name="isAvailable">Filter by availability status</param>
        /// <returns>Paginated list of donor profiles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDonorProfiles(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null,
            int? userId = null,
            int? bloodTypeId = null,
            string? healthStatus = null,
            bool? isAvailable = null)
        {
            var result = await _donorProfileService.GetAllDonorProfileAsync(
                pageNumber, pageSize, id, userId, bloodTypeId, healthStatus, isAvailable);

            if (result.IsSuccessed)
            {
                return Ok(BaseResponse<BasePaginatedList<DonorProfileModelView>>.OkResponse(result.ResultObj));
            }
            return BadRequest(new Blood.Core.APIResponse.ApiErrorResult<object>(result.Message));
        }

        /// <summary>
        /// Get donor profile by ID
        /// </summary>
        /// <param name="id">Donor profile ID</param>
        /// <returns>Donor profile details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDonorProfileById(int id)
        {
            var result = await _donorProfileService.GetDonorProfileByIdAsync(id);

            if (result.IsSuccessed)
            {
                return Ok(BaseResponse<DonorProfileModelView>.OkResponse(result.ResultObj));
            }

            return NotFound(new Blood.Core.APIResponse.ApiErrorResult<object>(result.Message));
        }

        /// <summary>
        /// Get donor profile by user ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Donor profile details</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDonorProfileByUserId(int userId)
        {
            var result = await _donorProfileService.GetDonorProfileByUserIdAsync(userId);

            if (result.IsSuccessed)
            {
                return Ok(BaseResponse<DonorProfileModelView>.OkResponse(result.ResultObj));
            }

            return NotFound(new Blood.Core.APIResponse.ApiErrorResult<object>(result.Message));
        }

        /// <summary>
        /// Create a new donor profile
        /// </summary>
        /// <param name="model">Donor profile creation data</param>
        /// <returns>Creation result</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDonorProfile([FromBody] CreateDonorProfileModelView model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(new Blood.Core.APIResponse.ApiErrorResult<object>("Invalid model data."));
            }

            var result = await _donorProfileService.AddDonorProfileAsync(model);

            if (result.IsSuccessed)
            {
                return CreatedAtAction(
                    nameof(GetDonorProfileById),
                    new { id = "created" },
                    BaseResponse<object>.OkResponse(result.Message));
            }

            return BadRequest(new Blood.Core.APIResponse.ApiErrorResult<object>(result.Message));
        }

        /// <summary>
        /// Update an existing donor profile
        /// </summary>
        /// <param name="id">Donor profile ID</param>
        /// <param name="model">Update data</param>
        /// <returns>Update result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonorProfile(int id, [FromBody] UpdateDonorProfileModelView model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(new Blood.Core.APIResponse.ApiErrorResult<object>("Invalid model data."));
            }

            var result = await _donorProfileService.UpdateDonorProfileAsync(id, model);

            if (result.IsSuccessed)
            {
                return Ok(BaseResponse<object>.OkResponse(result.Message));
            }

            return BadRequest(new Blood.Core.APIResponse.ApiErrorResult<object>(result.Message));
        }

        /// <summary>
        /// Delete a donor profile (soft delete)
        /// </summary>
        /// <param name="id">Donor profile ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonorProfile(int id)
        {
            var result = await _donorProfileService.DeleteDonorProfileAsync(id);

            if (result.IsSuccessed)
            {
                return Ok(BaseResponse<object>.OkResponse(result.Message));
            }

            return BadRequest(new Blood.Core.APIResponse.ApiErrorResult<object>(result.Message));
        }

        /// <summary>
        /// Get available donors for emergency situations
        /// </summary>
        /// <param name="bloodTypeId">Required blood type ID</param>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <returns>List of available emergency donors</returns>
        [HttpGet("emergency/available")]
        public async Task<IActionResult> GetEmergencyAvailableDonors(
            int? bloodTypeId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result = await _donorProfileService.GetAllDonorProfileAsync(
                pageNumber, pageSize, null, null, bloodTypeId, "eligible", true);

            if (result.IsSuccessed)
            {
                // Filter only emergency available donors
                var emergencyDonors = result.ResultObj.Items.Where(dp => dp.IsEmergencyAvailable).ToList();
                var emergencyResult = new BasePaginatedList<DonorProfileModelView>(
                    emergencyDonors, emergencyDonors.Count, pageNumber, pageSize);

                return Ok(BaseResponse<BasePaginatedList<DonorProfileModelView>>.OkResponse(emergencyResult));
            }

            return BadRequest(new Blood.Core.APIResponse.ApiErrorResult<object>(result.Message));
        }

        /// <summary>
        /// Get donors by blood type
        /// </summary>
        /// <param name="bloodTypeId">Blood type ID</param>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <returns>List of donors with specified blood type</returns>
        [HttpGet("bloodtype/{bloodTypeId}")]
        public async Task<IActionResult> GetDonorsByBloodType(
            int bloodTypeId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result = await _donorProfileService.GetAllDonorProfileAsync(
                pageNumber, pageSize, null, null, bloodTypeId, null, null);

            if (result.IsSuccessed)
            {
                return Ok(BaseResponse<BasePaginatedList<DonorProfileModelView>>.OkResponse(result.ResultObj));
            }

            return BadRequest(new Blood.Core.APIResponse.ApiErrorResult<object>(result.Message));
        }

        /// <summary>
        /// Get donor statistics
        /// </summary>
        /// <returns>Basic statistics about donors</returns>
        [HttpGet("statistics")]
        public async Task<IActionResult> GetDonorStatistics()
        {
            // Get all donors for statistics
            var allDonorsResult = await _donorProfileService.GetAllDonorProfileAsync(1, int.MaxValue, null, null, null, null, null);

            if (allDonorsResult.IsSuccessed)
            {
                var donors = allDonorsResult.ResultObj.Items;

                var statistics = new
                {
                    TotalDonors = donors.Count,
                    AvailableDonors = donors.Count(d => d.IsAvailable),
                    EmergencyAvailableDonors = donors.Count(d => d.IsEmergencyAvailable),
                    EligibleDonors = donors.Count(d => d.HealthStatus == "eligible"),
                    TotalDonations = donors.Sum(d => d.DonationCount),
                    AverageDonationsPerDonor = donors.Count > 0 ? donors.Average(d => d.DonationCount) : 0,
                    BloodTypeDistribution = donors.GroupBy(d => d.BloodTypeName)
                        .Select(g => new { BloodType = g.Key, Count = g.Count() })
                        .OrderByDescending(x => x.Count)
                        .ToList()
                };

                return Ok(BaseResponse<object>.OkResponse(statistics));
            }

            return BadRequest(new Blood.Core.APIResponse.ApiErrorResult<object>(allDonorsResult.Message));
        }
    }
}