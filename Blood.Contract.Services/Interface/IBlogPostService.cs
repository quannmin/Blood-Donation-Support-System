using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.BlogPostModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Services.Interface
{
    public interface IBlogPostService
    {
        Task<ApiResult<BasePaginatedList<BlogPostModelView>>> GetAllAsync(int pageNumber, int pageSize, string? keyword);
        Task<ApiResult<List<BlogPostModelView>>> GetAllWithoutPagingAsync(string? title);
        Task<ApiResult<BlogPostModelView>> GetByIdAsync(int id);
        Task<ApiResult<object>> AddAsync(BlogPostCreateModelView model);
        Task<ApiResult<object>> UpdateAsync(int id, BlogPostUpdateModelView model);
        Task<ApiResult<object>> DeleteAsync(int id);
    }

}
