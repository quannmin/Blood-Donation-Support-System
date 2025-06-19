using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.Core.Utils;
using Blood.Core;
using Blood.ModelViews.BloodRequestModelViews;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Blood.Core.Utils.SystemConstant;

namespace Blood.Services.Service
{
    // BloodRequestService
    public class BloodRequestService : IBloodRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public BloodRequestService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<BloodRequestModelView>>> GetAllAsync(int pageNumber, int pageSize, string? requestSource, DateTime? requestDate, string? status, string? bloodComponent, int? requestId, string? fullName)
        {
            var query = _unitOfWork.GetRepository<BloodRequest>().Entities
                .Include(x => x.BloodGroup)
                .Include(x => x.RequestedBy)
                .Include(x => x.BloodUnit)
                .AsNoTracking()
                .Where(x => !x.DeletedTime.HasValue);

            if (!string.IsNullOrWhiteSpace(requestSource))
                query = query.Where(x => x.RequestSource.Contains(requestSource));

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(x => x.Status.Contains(status));

            if (!string.IsNullOrWhiteSpace(bloodComponent))
                query = query.Where(x => x.BloodComponent.Contains(bloodComponent));

            if (!string.IsNullOrWhiteSpace(fullName))
                query = query.Where(x => x.RequestedBy.FullName.Contains(fullName));

            if (requestId.HasValue)
                query = query.Where(x => x.RequestedBy.Id == requestId);

            if (requestDate.HasValue)
                query = query.Where(x => x.RequestDate.Date == requestDate.Value.Date);

            int totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(x => x.RequestDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<BloodRequestModelView>>(items);
            return new ApiSuccessResult<BasePaginatedList<BloodRequestModelView>>(
                new BasePaginatedList<BloodRequestModelView>(result, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<BloodRequestModelView>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<BloodRequest>().Entities
                .Include(x => x.BloodGroup)
                .Include(x => x.RequestedBy)
                .Include(x => x.BloodUnit)
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (entity == null)
                return new ApiErrorResult<BloodRequestModelView>("Blood request not found.");

            var view = _mapper.Map<BloodRequestModelView>(entity);
            return new ApiSuccessResult<BloodRequestModelView>(view);
        }

        public async Task<ApiResult<List<BloodRequestModelView>>> GetAllWithoutPagingAsync(string? keyword)
        {
            var query = _unitOfWork.GetRepository<BloodRequest>().Entities
                .Include(x => x.BloodGroup)
                .Include(x => x.RequestedBy)
                .Include(x => x.BloodUnit)
                .Where(x => !x.DeletedTime.HasValue);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Notes.Contains(keyword) || x.RequestedBy.FullName.Contains(keyword));
            }

            var resultList = _mapper.Map<List<BloodRequestModelView>>(await query.ToListAsync());
            return new ApiSuccessResult<List<BloodRequestModelView>>(resultList);
        }

        public async Task<ApiResult<object>> CreateAsync(CreateBloodRequestModelView model)
        {
            var recipientBloodGroup = await _unitOfWork.GetRepository<BloodGroup>()
                .GetByIdAsync(model.BloodGroupId);
            if (recipientBloodGroup == null)
                return new ApiErrorResult<object>("Recipient blood group not found.");

            if (model.RequestSource == "FromStock")
            {
                if (model.BloodUnitId != null)
                {

                    var bloodUnit = await _unitOfWork.GetRepository<BloodUnit>()
                        .GetByIdAsync(model.BloodUnitId.Value);

                    if (bloodUnit == null || bloodUnit.DeletedTime != null)
                        return new ApiErrorResult<object>("Blood unit not found.");

                    if (bloodUnit.ExpiryDate < DateTime.Now)
                        return new ApiErrorResult<object>("Blood unit is expired.");

                    var compatibility = await _unitOfWork.GetRepository<BloodCompatibility>().Entities
                        .FirstOrDefaultAsync(x =>
                            x.DonorBloodGroupId == bloodUnit.BloodGroupId &&
                            x.RecipientBloodGroupId == model.BloodGroupId &&
                            x.BloodComponent == model.BloodComponent &&
                            x.IsCompatible);

                    if (compatibility == null)
                        return new ApiErrorResult<object>("Incompatible blood group or component.");

                    if (bloodUnit.Quantity < model.QuantityFromStock)
                        return new ApiErrorResult<object>("Not enough blood units in stock.");
                }
            }

            var request = _mapper.Map<BloodRequest>(model);
            request.Status = BloodRequestStatus.Pending;
            request.RequestDate = DateTime.Now;
            request.CreatedTime = DateTime.Now;
            request.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<BloodRequest>().InsertAsync(request);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood request created successfully.");
        }


        public async Task<ApiResult<object>> UpdateAsync(int id, UpdateBloodRequestModelView model)
        {
            var entity = await _unitOfWork.GetRepository<BloodRequest>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Blood request not found.");

            if (model.BloodGroupId.HasValue)
                entity.BloodGroupId = model.BloodGroupId.Value;

            if (!string.IsNullOrWhiteSpace(model.BloodComponent))
                entity.BloodComponent = model.BloodComponent;

            if (model.Quantity.HasValue)
                entity.Quantity = model.Quantity.Value;

            if (model.IsEmergency.HasValue)
                entity.IsEmergency = model.IsEmergency.Value;

            if (!string.IsNullOrWhiteSpace(model.Status))
                entity.Status = model.Status;

            if (model.Status == BloodRequestStatus.Fulfilled)
            {
                entity.FulfilledDate = DateTime.Now;
            }

            if (model.RequestedById.HasValue)
                entity.RequestedById = model.RequestedById.Value;

            if (!string.IsNullOrWhiteSpace(model.Notes))
                entity.Notes = model.Notes;

            if (!string.IsNullOrWhiteSpace(model.RequestSource))
                entity.RequestSource = model.RequestSource;

            if ((model.BloodUnitId.HasValue || model.QuantityFromStock.HasValue) && entity.Status != BloodRequestStatus.Fulfilled)
            {
                return new ApiErrorResult<object>("BloodUnitId and QuantityFromStock should only be provided when the request status is 'Fulfilled'.");
            }


            if (entity.RequestSource == "FromStock" && entity.Status == BloodRequestStatus.Fulfilled)
            {
                if (!model.BloodUnitId.HasValue || !model.QuantityFromStock.HasValue)
                    return new ApiErrorResult<object>("BloodUnitId and QuantityFromStock are required for FromStock.");

                var bloodUnit = await _unitOfWork.GetRepository<BloodUnit>().GetByIdAsync(model.BloodUnitId.Value);

                if (bloodUnit == null || bloodUnit.DeletedTime != null)
                    return new ApiErrorResult<object>("Blood unit not found.");

                if (bloodUnit.ExpiryDate < DateTime.Now)
                    return new ApiErrorResult<object>("Blood unit is expired.");

                if (bloodUnit.BloodComponent != entity.BloodComponent)
                    return new ApiErrorResult<object>("Blood component mismatch between request and blood unit.");

                var compatibility = await _unitOfWork.GetRepository<BloodCompatibility>().Entities
                    .FirstOrDefaultAsync(x =>
                        x.DonorBloodGroupId == bloodUnit.BloodGroupId &&
                        x.RecipientBloodGroupId == entity.BloodGroupId &&
                        x.BloodComponent == entity.BloodComponent &&
                        x.IsCompatible);

                if (compatibility == null)
                    return new ApiErrorResult<object>("Incompatible blood group or component.");

                if (bloodUnit.Quantity < model.QuantityFromStock.Value)
                    return new ApiErrorResult<object>("Not enough blood units in stock.");

                bloodUnit.Quantity -= model.QuantityFromStock.Value;
                _unitOfWork.GetRepository<BloodUnit>().Update(bloodUnit);

                entity.BloodUnitId = model.BloodUnitId;
                entity.QuantityFromStock = model.QuantityFromStock;
            }
            else if (model.RequestSource == "FromDonor")
            {
                entity.BloodUnitId = null;
                entity.QuantityFromStock = null;
            }

            entity.LastUpdatedTime = DateTime.Now;
            entity.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            _unitOfWork.GetRepository<BloodRequest>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood request updated successfully.");
        }

        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<BloodRequest>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Blood request not found.");

            entity.DeletedTime = DateTime.Now;
            entity.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            _unitOfWork.GetRepository<BloodRequest>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Blood request deleted successfully.");
        }

        public async Task<ApiResult<List<BloodRequestModelView>>> GetAllWithoutPagingAsync()
        {
            var query = _unitOfWork.GetRepository<BloodRequest>().Entities
                .Include(x => x.BloodGroup)
                .Include(x => x.RequestedBy)
                .Include(x => x.BloodUnit)
                .AsNoTracking()
                .Where(x => !x.DeletedTime.HasValue);


            var result = _mapper.Map<List<BloodRequestModelView>>(query);
            return new ApiSuccessResult<List<BloodRequestModelView>>(result);
        }
    }

}
