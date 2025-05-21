using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Blood.Contract.Services.Interface;
using Blood.Contract.Repositories.Interface;
using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.RoleModelViews;
using Blood.Contract.Repositories.Entity;

namespace Blood.Services.Service
{
	public class RoleService : IRoleService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _contextAccessor;

		public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_contextAccessor = contextAccessor;
		}

		public async Task<ApiResult<BasePaginatedList<RoleModelView>>> GetAllRoleAsync(int pageNumber, int pageSize, int? id, string? name)
		{
			IQueryable<ApplicationRole> roleQuery = _unitOfWork.GetRepository<ApplicationRole>().Entities
				.AsNoTracking()
                .OrderByDescending(cb => cb.CreatedTime)
                .Where(p => !p.DeletedTime.HasValue);

			if (id != null)
				roleQuery = roleQuery.Where(p => p.Id == id);

			if (!string.IsNullOrWhiteSpace(name))
				roleQuery = roleQuery.Where(p => p.Name.Contains(name));

			roleQuery = roleQuery.OrderByDescending(r => r.CreatedTime);

			int totalCount = await roleQuery.CountAsync();

			List<ApplicationRole> paginatedRoles = await roleQuery
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			List<RoleModelView> roleModelViews = _mapper.Map<List<RoleModelView>>(paginatedRoles);
			var result = new BasePaginatedList<RoleModelView>(roleModelViews, totalCount, pageNumber, pageSize);

			return new ApiSuccessResult<BasePaginatedList<RoleModelView>>(result);
		}

		public async Task<ApiResult<object>> AddRoleAsync(CreateRoleModelView model)
		{
			var existedRole = await _unitOfWork.GetRepository<ApplicationRole>()
				.Entities
				.FirstOrDefaultAsync(role => role.Name.Equals(model.Name) && !role.DeletedTime.HasValue);

			if (existedRole != null)
			{
				return new ApiErrorResult<object>("Role already exists");
			}

			ApplicationRole newRole = _mapper.Map<ApplicationRole>(model);

			newRole.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0"); ;
			newRole.CreatedTime = DateTime.Now;

			await _unitOfWork.GetRepository<ApplicationRole>().InsertAsync(newRole);

			await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Role added successfully");

		}

		public async Task<ApiResult<object>> UpdateRoleAsync(int id, UpdatedRoleModelView model)
		{
			if (id == null)
			{
                return new ApiErrorResult<object>("Please provide a valid Role ID.");
			}

			var existingRole = await _unitOfWork.GetRepository<ApplicationRole>().Entities
				.FirstOrDefaultAsync(s => s.Id == id && !s.DeletedTime.HasValue);

			if (existingRole == null)
			{
                return new ApiErrorResult<object>("The Role cannot be found or has been deleted!");

			}

			bool isUpdated = false;

			if (!string.IsNullOrWhiteSpace(model.Name) && model.Name != existingRole.Name)
			{
				var roleWithSameName = await _unitOfWork.GetRepository<ApplicationRole>().Entities
					.AnyAsync(s => s.Name == model.Name && !s.DeletedTime.HasValue);

				if (roleWithSameName)
				{
                    return new ApiErrorResult<object>("A role with the same name already exists.");

				}

				existingRole.Name = model.Name;
				isUpdated = true;
			}

			if (isUpdated)
			{
				existingRole.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0"); ;
				existingRole.LastUpdatedTime = DateTime.Now;

				await _unitOfWork.GetRepository<ApplicationRole>().UpdateAsync(existingRole);
				await _unitOfWork.SaveAsync();
			}
            return new ApiSuccessResult<object>("Role successfully updated.");

		}

		public async Task<ApiResult<object>> DeleteRoleAsync(int id)
		{
			if (id == null)
			{
                return new ApiErrorResult<object>("Please provide a valid Role ID.");

			}

			var existingRole = await _unitOfWork.GetRepository<ApplicationRole>().Entities
				.FirstOrDefaultAsync(s => s.Id == id && !s.DeletedTime.HasValue);

			if (existingRole == null)
			{
                return new ApiErrorResult<object>("The Role cannot be found or has been deleted!");

			}

			existingRole.DeletedTime = DateTime.Now;
			existingRole.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0"); ;

			await _unitOfWork.GetRepository<ApplicationRole>().UpdateAsync(existingRole);
			await _unitOfWork.SaveAsync();
            return new ApiSuccessResult<object>("Role successfully deleted.");

		}

		public async Task<ApiResult<RoleModelView>> GetRoleByIdAsync(int id)
		{
			if (id == null)
			{
                return new ApiErrorResult<RoleModelView>("Please provide a valid Role ID.");

            }

			var roleEntity = await _unitOfWork.GetRepository<ApplicationRole>().Entities
				.FirstOrDefaultAsync(role => role.Id == id && !role.DeletedTime.HasValue);

			if (roleEntity == null)
			{
                return new ApiErrorResult<RoleModelView>("Role is not exited.");

			}

			RoleModelView roleModelView = _mapper.Map<RoleModelView>(roleEntity);
            return new ApiSuccessResult<RoleModelView>(roleModelView);

		}

    }
}