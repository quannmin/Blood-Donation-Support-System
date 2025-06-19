using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.ModelViews.BlogPostModelViews;
using Microsoft.AspNetCore.Mvc;

namespace BloodBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        /// <summary>
        /// Get all blog posts with pagination and optional keyword filter
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPaging(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            try
            {
                var result = await _blogPostService.GetAllAsync(pageNumber, pageSize, keyword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get all blog posts without pagination (optional title filter)
        /// </summary>
        [HttpGet("all/nopaging")]
        public async Task<IActionResult> GetAllWithoutPaging([FromQuery] string? title = null)
        {
            try
            {
                var result = await _blogPostService.GetAllWithoutPagingAsync(title);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Get a blog post by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _blogPostService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Create a new blog post
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] BlogPostCreateModelView model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _blogPostService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Update a blog post
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] BlogPostUpdateModelView model)
        {
            try
            {
                var result = await _blogPostService.UpdateAsync(id, model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }

        /// <summary>
        /// Delete a blog post
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _blogPostService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResult<object>(ex.Message));
            }
        }
    }

}
