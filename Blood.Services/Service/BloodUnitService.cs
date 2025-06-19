using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.BloodUnitModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Blood.ModelViews.BloodGroupModelViews;

namespace Blood.Services.Service
{
    public class BloodUnitService : IBloodUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public BloodUnitService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<BloodUnitModelView>>> GetAllAsync(int pageNumber, int pageSize, string? bloodGroupName, string? bloodComponent)
        {
            var query = _unitOfWork.GetRepository<BloodUnit>().Entities
                .Include(x => x.BloodGroup)
                .AsNoTracking()
                .Where(x => !x.DeletedTime.HasValue);

            if (!string.IsNullOrWhiteSpace(bloodGroupName))
                query = query.Where(x => x.BloodGroup.Name.Contains(bloodGroupName));

            if (!string.IsNullOrWhiteSpace(bloodComponent))
                query = query.Where(x => x.BloodComponent.Contains(bloodComponent));

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var modelViews = _mapper.Map<List<BloodUnitModelView>>(items);

            for (int i = 0; i < modelViews.Count; i++)
            {
                modelViews[i].BloodGroup = _mapper.Map<BloodGroupModelView>(items[i].BloodGroup);
            }

            var result = new BasePaginatedList<BloodUnitModelView>(modelViews, totalCount, pageNumber, pageSize);
            return new ApiSuccessResult<BasePaginatedList<BloodUnitModelView>>(result);
        }

        public async Task<ApiResult<List<BloodUnitModelView>>> GetAllWithoutPagingAsync()
        {
            var query = _unitOfWork.GetRepository<BloodUnit>().Entities
                .Include(x => x.BloodGroup)
                .AsNoTracking()
                .Where(x => !x.DeletedTime.HasValue);

            var items = await query
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync();

            var modelViews = _mapper.Map<List<BloodUnitModelView>>(items);

            for (int i = 0; i < modelViews.Count; i++)
            {
                modelViews[i].BloodGroup = _mapper.Map<BloodGroupModelView>(items[i].BloodGroup);
            }

            return new ApiSuccessResult<List<BloodUnitModelView>>(modelViews);
        }

        public async Task<ApiResult<BloodUnitModelView>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<BloodUnit>().Entities
                .Include(x => x.BloodGroup)
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (entity == null)
                return new ApiErrorResult<BloodUnitModelView>("Blood unit not found");

            var modelView = _mapper.Map<BloodUnitModelView>(entity);

            modelView.BloodGroup = _mapper.Map<BloodGroupModelView>(entity.BloodGroup);

            return new ApiSuccessResult<BloodUnitModelView>(modelView);
        }

        public async Task<ApiResult<object>> AddAsync(CreateBloodUnitModelView model)
        {
            var entity = _mapper.Map<BloodUnit>(model);
            entity.CreatedTime = DateTime.Now;
            entity.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<BloodUnit>().InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood unit created successfully");
        }

        public async Task<ApiResult<object>> UpdateAsync(int id, UpdateBloodUnitModelView model)
        {
            var entity = await _unitOfWork.GetRepository<BloodUnit>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Blood unit not found");

            if (model.BloodGroupId.HasValue)
                entity.BloodGroupId = model.BloodGroupId.Value;

            if (!string.IsNullOrWhiteSpace(model.BloodComponent))
                entity.BloodComponent = model.BloodComponent;

            if (model.Quantity.HasValue)
                entity.Quantity = model.Quantity.Value;

            if (model.ExpiryDate.HasValue)
                entity.ExpiryDate = model.ExpiryDate.Value;

            entity.LastUpdatedTime = DateTime.Now;
            entity.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            _unitOfWork.GetRepository<BloodUnit>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood unit updated successfully");
        }

        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<BloodUnit>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Blood unit not found");

            entity.DeletedTime = DateTime.Now;
            entity.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            _unitOfWork.GetRepository<BloodUnit>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood unit deleted successfully");
        }
    }


}
