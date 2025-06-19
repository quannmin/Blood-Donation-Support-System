using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.DonationModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Services.Interface
{
    public interface IDonationService
    {
        Task<ApiResult<List<DonationModelView>>> GetAllWithoutPagingAsync();
        Task<ApiResult<BasePaginatedList<DonationModelView>>> GetAllAsync(int pageNumber, int pageSize, int? userId, int? bloodRequestId, DateTime? donationDate);
        Task<ApiResult<DonationModelView>> GetByIdAsync(int id);
        Task<ApiResult<object>> CreateAsync(CreateDonationModelView model);
        Task<ApiResult<object>> UpdateAsync(int id, UpdateDonationModelView model);
        Task<ApiResult<object>> DeleteAsync(int id);
    }
}
