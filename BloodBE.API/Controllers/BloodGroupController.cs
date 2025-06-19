using Microsoft.AspNetCore.Mvc;
using Blood.Contract.Services.Interface;
using Blood.Core;
using Blood.ModelViews.BloodGroupModelViews;
using Blood.Core.APIResponse;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodGroupController : ControllerBase
    {
        private readonly IBloodGroupService _bloodGroupService;

        public BloodGroupController(IBloodGroupService bloodGroupService)
        {
            _bloodGroupService = bloodGroupService;
        }

        /// <summary>
        ///     Get all blood groups with pagination and optional filters
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<BasePaginatedList<BloodGroupModelView>>> GetAllBloodGroups
            ([FromQuery] int? id, [FromQuery] string? name, int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var result = await _bloodGroupService.GetAllAsync(pageNumber, pageSize, id, name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Get a blood group by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodGroupModelView>> GetBloodGroupById(int id)
        {
            try
            {
                var result = await _bloodGroupService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Create a new blood group
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<object>> CreateBloodGroup([FromQuery] CreateBloodGroupModelView model)
        {
            try
            {
                var result = await _bloodGroupService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Update a blood group
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<object>> UpdateBloodGroup(int id, [FromQuery] UpdateBloodGroupModelView model)
        {
            try
            {
                var result = await _bloodGroupService.UpdateAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     Delete a blood group
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<object>> DeleteBloodGroup(int id)
        {
            try
            {
                var result = await _bloodGroupService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        ///     All a blood group
        /// </summary>
        [HttpGet("all/nopaging")]
        public async Task<IActionResult> GetAllWithoutPaging()
        {           
            try
            {
                var result = await _bloodGroupService.GetAllWithoutPagingAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }
}
