using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.BloodGroupModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Services.Interface
{
    public interface IBloodGroupService
    {
        Task<ApiResult<BasePaginatedList<BloodGroupModelView>>> GetAllAsync(int pageNumber, int pageSize, int? id, string? name);
        Task<ApiResult<object>> AddAsync(CreateBloodGroupModelView model);
        Task<ApiResult<object>> UpdateAsync(int id, UpdateBloodGroupModelView model);
        Task<ApiResult<object>> DeleteAsync(int id);
        Task<ApiResult<BloodGroupModelView>> GetByIdAsync(int id);

        Task<ApiResult<List<BloodGroupModelView>>> GetAllWithoutPagingAsync();
    }
}
