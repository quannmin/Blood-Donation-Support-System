using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.BloodCompatibilityModelViews;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blood.ModelViews.BloodGroupModelViews;

namespace Blood.Services.Service
{
    public class BloodCompatibilityService : IBloodCompatibilityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public BloodCompatibilityService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<BloodCompatibilityModelView>>> GetAllAsync(
                int pageNumber, int pageSize,
                string? bloodComponent,
                string? donorBloodGroupName,
                string? recipientBloodGroupName)
        {
            var query = _unitOfWork.GetRepository<BloodCompatibility>().Entities
                .Include(x => x.DonorBloodGroup)
                .Include(x => x.RecipientBloodGroup)
                .Where(x => !x.DeletedTime.HasValue);

            // Filtering logic
            if (!string.IsNullOrWhiteSpace(bloodComponent))
                query = query.Where(x => x.BloodComponent.Contains(bloodComponent));

            if (!string.IsNullOrWhiteSpace(donorBloodGroupName))
                query = query.Where(x => x.DonorBloodGroup.Name.Contains(donorBloodGroupName));

            if (!string.IsNullOrWhiteSpace(recipientBloodGroupName))
                query = query.Where(x => x.RecipientBloodGroup.Name.Contains(recipientBloodGroupName));

            // Total after filtering
            int totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var modelViews = _mapper.Map<List<BloodCompatibilityModelView>>(items);

            for (int i = 0; i < modelViews.Count; i++)
            {
                modelViews[i].DonorBloodGroupModelView = _mapper.Map<BloodGroupModelView>(items[i].DonorBloodGroup);
                modelViews[i].RecipientBloodGroupModelView = _mapper.Map<BloodGroupModelView>(items[i].RecipientBloodGroup);
            }

            return new ApiSuccessResult<BasePaginatedList<BloodCompatibilityModelView>>(
                new BasePaginatedList<BloodCompatibilityModelView>(modelViews, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<BloodCompatibilityModelView>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<BloodCompatibility>().Entities
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (entity == null)
                return new ApiErrorResult<BloodCompatibilityModelView>("Blood compatibilityp not found");

            var modelView = _mapper.Map<BloodCompatibilityModelView>(entity);

            modelView.DonorBloodGroupModelView = _mapper.Map<BloodGroupModelView>(entity.DonorBloodGroup);

            modelView.RecipientBloodGroupModelView = _mapper.Map<BloodGroupModelView>(entity.RecipientBloodGroup);

            return entity == null
                ? new ApiErrorResult<BloodCompatibilityModelView>("Not found")
                : new ApiSuccessResult<BloodCompatibilityModelView>(modelView);
        }

        public async Task<ApiResult<object>> AddAsync(CreateBloodCompatibilityModelView model)
        {
            var entity = _mapper.Map<BloodCompatibility>(model);
            entity.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            entity.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<BloodCompatibility>().InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ApiSuccessResult<object>("Added successfully");
        }

        public async Task<ApiResult<object>> UpdateAsync(int id, UpdateBloodCompatibilityModelView model)
        {
            var entity = await _unitOfWork.GetRepository<BloodCompatibility>().Entities
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (entity == null)
                return new ApiErrorResult<object>("BloodCompatibility not found.");

            bool isUpdated = false;

            if (model.DonorBloodGroupId.HasValue && model.DonorBloodGroupId != entity.DonorBloodGroupId)
            {
                entity.DonorBloodGroupId = model.DonorBloodGroupId.Value;
                isUpdated = true;
            }

            if (model.RecipientBloodGroupId.HasValue && model.RecipientBloodGroupId != entity.RecipientBloodGroupId)
            {
                entity.RecipientBloodGroupId = model.RecipientBloodGroupId.Value;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(model.BloodComponent) && model.BloodComponent != entity.BloodComponent)
            {
                entity.BloodComponent = model.BloodComponent;
                isUpdated = true;
            }

            if (model.IsCompatible.HasValue && model.IsCompatible != entity.IsCompatible)
            {
                entity.IsCompatible = model.IsCompatible.Value;
                isUpdated = true;
            }

            if (!isUpdated)
                return new ApiErrorResult<object>("No changes detected.");

            entity.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            entity.LastUpdatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<BloodCompatibility>().UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("BloodCompatibility updated successfully.");
        }


        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<BloodCompatibility>().Entities
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (entity == null) return new ApiErrorResult<object>("Not found");

            entity.DeletedTime = DateTime.Now;
            entity.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<BloodCompatibility>().UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ApiSuccessResult<object>("Deleted successfully");
        }
    }
}
