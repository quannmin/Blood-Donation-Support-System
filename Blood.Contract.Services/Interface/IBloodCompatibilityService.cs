using Blood.Contract.Repositories.Entity;
using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.BloodCompatibilityModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Services.Interface
{
    public interface IBloodCompatibilityService
    {
        Task<ApiResult<BasePaginatedList<BloodCompatibilityModelView>>> GetAllAsync(
                int pageNumber, int pageSize,
                string? bloodComponent,
                string? donorBloodGroupName,
                string? recipientBloodGroupName);
        Task<ApiResult<BloodCompatibilityModelView>> GetByIdAsync(int id);
        Task<ApiResult<object>> AddAsync(CreateBloodCompatibilityModelView model);
        Task<ApiResult<object>> UpdateAsync(int id, UpdateBloodCompatibilityModelView model);
        Task<ApiResult<object>> DeleteAsync(int id);
    }
}
