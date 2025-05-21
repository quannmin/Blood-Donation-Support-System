using Blood.Core;
using Blood.Core.APIResponse;
using Blood.ModelViews.RoleModelViews;

namespace Blood.Contract.Services.Interface
{
    public interface IRoleService
    {
        Task<ApiResult<BasePaginatedList<RoleModelView>>> GetAllRoleAsync(int pageNumber, int pageSize, int? id, string? name);
        Task<ApiResult<object>> AddRoleAsync(CreateRoleModelView model);
        Task<ApiResult<object>> UpdateRoleAsync(int id, UpdatedRoleModelView model);
        Task<ApiResult<object>> DeleteRoleAsync(int id);
        Task<ApiResult<RoleModelView>> GetRoleByIdAsync(int id);

    }
}
