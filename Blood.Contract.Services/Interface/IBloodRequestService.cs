using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.BloodRequestModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Services.Interface
{
    public interface IBloodRequestService
    {
        Task<ApiResult<List<BloodRequestModelView>>> GetAllWithoutPagingAsync();

        Task<ApiResult<BasePaginatedList<BloodRequestModelView>>> GetAllAsync(
        int pageNumber, int pageSize,
        string? requestSource,
        DateTime? requestDate,
        string? status,
        string? bloodComponent,
        int? requestId, 
        string? fullName);
        Task<ApiResult<BloodRequestModelView>> GetByIdAsync(int id);
        Task<ApiResult<object>> CreateAsync(CreateBloodRequestModelView model);
        Task<ApiResult<object>> UpdateAsync(int id, UpdateBloodRequestModelView model);
        Task<ApiResult<object>> DeleteAsync(int id);
    }
}
