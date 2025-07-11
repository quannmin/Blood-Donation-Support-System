using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.DonorAvailabilityModelViews;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blood.Services.Service
{
    public class DonorAvailabilityService : IDonorAvailabilityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _context;

        public DonorAvailabilityService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResult<object>> AddAsync(CreateDonorAvailabilityModelView model)
        {
            if (model.AvailableFrom >= model.AvailableTo)
                return new ApiErrorResult<object>("AvailableFrom must be earlier than AvailableTo");

            var entity = _mapper.Map<DonorAvailability>(model);
            entity.CreatedTime = DateTime.Now;
            entity.CreatedBy = int.Parse(_context.HttpContext?.User?.FindFirst("userId")?.Value ?? model.UserId.ToString());

            await _unitOfWork.GetRepository<DonorAvailability>().InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Donor availability created successfully");
        }

        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<DonorAvailability>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Availability not found");

            entity.DeletedTime = DateTime.Now;
            entity.DeletedBy = int.Parse(_context.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            _unitOfWork.GetRepository<DonorAvailability>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Deleted successfully");
        }

        public async Task<ApiResult<BasePaginatedList<DonorAvailabilityModelView>>> GetAllAsync(int pageNumber, int pageSize, string? userName)
        {
            var query = _unitOfWork.GetRepository<DonorAvailability>().Entities
                .Include(x => x.User)
                .Where(x => !x.DeletedTime.HasValue);

            if (!string.IsNullOrWhiteSpace(userName))
                query = query.Where(x => x.User.FullName.Contains(userName));

            int total = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<DonorAvailabilityModelView>>(items);
            return new ApiSuccessResult<BasePaginatedList<DonorAvailabilityModelView>>(
                new BasePaginatedList<DonorAvailabilityModelView>(result, total, pageNumber, pageSize)
            );
        }

        public async Task<ApiResult<List<DonorAvailabilityModelView>>> GetAllWithoutPagingAsync()
        {
            var items = await _unitOfWork.GetRepository<DonorAvailability>().Entities
                .Include(x => x.User)
                .Where(x => !x.DeletedTime.HasValue)
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync();

            var result = _mapper.Map<List<DonorAvailabilityModelView>>(items);
            return new ApiSuccessResult<List<DonorAvailabilityModelView>>(result);
        }

        public async Task<ApiResult<DonorAvailabilityModelView>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<DonorAvailability>().Entities
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (entity == null)
                return new ApiErrorResult<DonorAvailabilityModelView>("Not found");

            var model = _mapper.Map<DonorAvailabilityModelView>(entity);
            return new ApiSuccessResult<DonorAvailabilityModelView>(model);
        }

        public async Task<ApiResult<object>> UpdateAsync(int id, UpdateDonorAvailabilityModelView model)
        {
            var entity = await _unitOfWork.GetRepository<DonorAvailability>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Not found");

            if (model.UserId.HasValue)
                entity.UserId = model.UserId.Value;

            if (model.AvailableFrom.HasValue)
                entity.AvailableFrom = model.AvailableFrom.Value;

            if (model.AvailableTo.HasValue)
                entity.AvailableTo = model.AvailableTo.Value;

            if (model.IsActive.HasValue)
                entity.IsActive = model.IsActive.Value;

            entity.LastUpdatedTime = DateTime.Now;
            entity.LastUpdatedBy = int.Parse(_context.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            _unitOfWork.GetRepository<DonorAvailability>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Updated successfully");
        }

        public async Task<ApiResult<DonorAvailabilityModelView>> GetDonorAvailabilityByUserIdAsync(int userId)
        {
            var entity = await _unitOfWork.GetRepository<DonorAvailability>().Entities
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId && !x.DeletedTime.HasValue);

            if (entity == null)
                return new ApiErrorResult<DonorAvailabilityModelView>("Donor availability not found for this user");

            var model = _mapper.Map<DonorAvailabilityModelView>(entity);
            return new ApiSuccessResult<DonorAvailabilityModelView>(model);
        }
    }

}
