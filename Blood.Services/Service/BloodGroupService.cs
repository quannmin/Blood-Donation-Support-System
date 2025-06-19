using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.BloodGroupModelViews;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blood.Services.Service
{
    public class BloodGroupService : IBloodGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public BloodGroupService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<BloodGroupModelView>>> GetAllAsync(int pageNumber, int pageSize, int? id, string? name)
        {
            var query = _unitOfWork.GetRepository<BloodGroup>().Entities
                .AsNoTracking()
                .Where(bg => !bg.DeletedTime.HasValue);

            if (id.HasValue)
                query = query.Where(bg => bg.Id == id);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(bg => bg.Name.Contains(name));

            int totalCount = await query.CountAsync();
            var bloodGroups = await query
                .OrderByDescending(bg => bg.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var modelViews = _mapper.Map<List<BloodGroupModelView>>(bloodGroups);
            var result = new BasePaginatedList<BloodGroupModelView>(modelViews, totalCount, pageNumber, pageSize);

            return new ApiSuccessResult<BasePaginatedList<BloodGroupModelView>>(result);
        }

        public async Task<ApiResult<object>> AddAsync(CreateBloodGroupModelView model)
        {
            var exists = await _unitOfWork.GetRepository<BloodGroup>().Entities
                .AnyAsync(bg => bg.Name == model.Name && !bg.DeletedTime.HasValue);

            if (exists)
                return new ApiErrorResult<object>("Blood group already exists");

            var newBloodGroup = _mapper.Map<BloodGroup>(model);
            newBloodGroup.CreatedTime = DateTime.Now;
            newBloodGroup.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<BloodGroup>().InsertAsync(newBloodGroup);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood group added successfully");
        }

        public async Task<ApiResult<object>> UpdateAsync(int id, UpdateBloodGroupModelView model)
        {
            var bloodGroup = await _unitOfWork.GetRepository<BloodGroup>().Entities
                .FirstOrDefaultAsync(bg => bg.Id == id && !bg.DeletedTime.HasValue);

            if (bloodGroup == null)
                return new ApiErrorResult<object>("Blood group not found");

            if (!string.IsNullOrWhiteSpace(model.Name) && model.Name != bloodGroup.Name)
            {
                var exists = await _unitOfWork.GetRepository<BloodGroup>().Entities
                    .AnyAsync(bg => bg.Name == model.Name && !bg.DeletedTime.HasValue);

                if (exists)
                    return new ApiErrorResult<object>("Blood group name already exists");

                bloodGroup.Name = model.Name;
            }

            bloodGroup.LastUpdatedTime = DateTime.Now;
            bloodGroup.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<BloodGroup>().UpdateAsync(bloodGroup);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood group updated successfully");
        }

        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var bloodGroup = await _unitOfWork.GetRepository<BloodGroup>().Entities
                .FirstOrDefaultAsync(bg => bg.Id == id && !bg.DeletedTime.HasValue);

            if (bloodGroup == null)
                return new ApiErrorResult<object>("Blood group not found");

            bloodGroup.DeletedTime = DateTime.Now;
            bloodGroup.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<BloodGroup>().UpdateAsync(bloodGroup);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood group deleted successfully");
        }

        public async Task<ApiResult<BloodGroupModelView>> GetByIdAsync(int id)
        {
            var bloodGroup = await _unitOfWork.GetRepository<BloodGroup>().Entities
                .FirstOrDefaultAsync(bg => bg.Id == id && !bg.DeletedTime.HasValue);

            if (bloodGroup == null)
                return new ApiErrorResult<BloodGroupModelView>("Blood group not found");

            var modelView = _mapper.Map<BloodGroupModelView>(bloodGroup);
            return new ApiSuccessResult<BloodGroupModelView>(modelView);
        }

        public async Task<ApiResult<List<BloodGroupModelView>>> GetAllWithoutPagingAsync()
        {
            var query = _unitOfWork.GetRepository<BloodGroup>().Entities
                .AsNoTracking()
                .Where(bg => !bg.DeletedTime.HasValue);

            var bloodGroups = await query
                .OrderByDescending(bg => bg.CreatedTime)
                .ToListAsync();

            var modelViews = _mapper.Map<List<BloodGroupModelView>>(bloodGroups);

            return new ApiSuccessResult<List<BloodGroupModelView>>(modelViews);
        }

    }
}
