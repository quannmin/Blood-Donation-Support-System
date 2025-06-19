using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.BloodUnitModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Services.Interface
{
    public interface IBloodUnitService
    {
        Task<ApiResult<BasePaginatedList<BloodUnitModelView>>> GetAllAsync(int pageNumber, int pageSize, string? bloodGroupName, string? bloodComponent);
        Task<ApiResult<List<BloodUnitModelView>>> GetAllWithoutPagingAsync();
        Task<ApiResult<BloodUnitModelView>> GetByIdAsync(int id);
        Task<ApiResult<object>> AddAsync(CreateBloodUnitModelView model);
        Task<ApiResult<object>> UpdateAsync(int id, UpdateBloodUnitModelView model);
        Task<ApiResult<object>> DeleteAsync(int id);
    }

}
