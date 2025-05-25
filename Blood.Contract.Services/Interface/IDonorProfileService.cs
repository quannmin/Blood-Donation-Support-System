using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.DonorProfileViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Services.Interface
{
    public interface IDonorProfileService
    {
        Task<ApiResult<BasePaginatedList<DonorProfileModelView>>> GetAllDonorProfileAsync(
            int pageNumber, int pageSize, int? id, int? userId, int? bloodTypeId,
            string? healthStatus, bool? isAvailable);
        Task<ApiResult<object>> AddDonorProfileAsync(CreateDonorProfileModelView model);
        Task<ApiResult<object>> UpdateDonorProfileAsync(int id, UpdateDonorProfileModelView model);
        Task<ApiResult<object>> DeleteDonorProfileAsync(int id);
        Task<ApiResult<DonorProfileModelView>> GetDonorProfileByIdAsync(int id);
        Task<ApiResult<DonorProfileModelView>> GetDonorProfileByUserIdAsync(int userId);
    }
}
