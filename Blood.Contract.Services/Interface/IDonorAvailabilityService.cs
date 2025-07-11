using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.DonorAvailabilityModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Services.Interface
{
    public interface IDonorAvailabilityService
    {
        Task<ApiResult<BasePaginatedList<DonorAvailabilityModelView>>> GetAllAsync(int pageNumber, int pageSize, string? userName);
        Task<ApiResult<List<DonorAvailabilityModelView>>> GetAllWithoutPagingAsync();
        Task<ApiResult<DonorAvailabilityModelView>> GetByIdAsync(int id);
        Task<ApiResult<object>> AddAsync(CreateDonorAvailabilityModelView model);
        Task<ApiResult<object>> UpdateAsync(int id, UpdateDonorAvailabilityModelView model);
        Task<ApiResult<object>> DeleteAsync(int id);

        Task<ApiResult<DonorAvailabilityModelView>> GetDonorAvailabilityByUserIdAsync(int userId);
    }
}
