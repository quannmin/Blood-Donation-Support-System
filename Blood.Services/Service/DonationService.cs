using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core;
using Blood.Core.APIResponse;
using Blood.ModelViews.DonationModelViews;
using Blood.Repositories.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Blood.Core.Utils.SystemConstant;

namespace Blood.Services.Service
{
    public class DonationService : IDonationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public DonationService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<DonationModelView>>> GetAllAsync(int pageNumber, int pageSize, int? userId, int? bloodRequestId, DateTime? donationDate)
        {
            var query = _unitOfWork.GetRepository<Donation>().Entities
                .Include(x => x.User)
                .Include(x => x.BloodRequest)
                .Where(x => !x.DeletedTime.HasValue);

            if (userId.HasValue)
                query = query.Where(x => x.UserId == userId.Value);

            if (bloodRequestId.HasValue)
                query = query.Where(x => x.BloodRequestId == bloodRequestId.Value);

            if (donationDate.HasValue)
            {
                query = query.Where(x => x.DonationDate == donationDate.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var modelViews = _mapper.Map<List<DonationModelView>>(items);

            return new ApiSuccessResult<BasePaginatedList<DonationModelView>>(
                new BasePaginatedList<DonationModelView>(modelViews, totalCount, pageNumber, pageSize));
        }

        public async Task<ApiResult<List<DonationModelView>>> GetAllWithoutPagingAsync()
        {
            var items = await _unitOfWork.GetRepository<Donation>().Entities
                .Include(x => x.User)
                .Include(x => x.BloodRequest)
                .Where(x => !x.DeletedTime.HasValue)
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync();

            var modelViews = _mapper.Map<List<DonationModelView>>(items);
            return new ApiSuccessResult<List<DonationModelView>>(modelViews);
        }

        public async Task<ApiResult<DonationModelView>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<Donation>().Entities
                .Include(x => x.User)
                .Include(x => x.BloodRequest)
                .FirstOrDefaultAsync(x => x.Id == id && !x.DeletedTime.HasValue);

            if (entity == null)
                return new ApiErrorResult<DonationModelView>("Donation not found");

            var modelView = _mapper.Map<DonationModelView>(entity);
            return new ApiSuccessResult<DonationModelView>(modelView);
        }

        public async Task<ApiResult<object>> CreateAsync(CreateDonationModelView model)
        {
            var userRepo = _unitOfWork.GetRepository<ApplicationUser>();
            var requestRepo = _unitOfWork.GetRepository<BloodRequest>();
            var donationRepo = _unitOfWork.GetRepository<Donation>();
            var availabilityRepo = _unitOfWork.GetRepository<DonorAvailability>();

            var user = await userRepo.GetByIdAsync(model.UserId);
            if (user == null || user.DeletedTime != null)
                return new ApiErrorResult<object>("User not found.");

            var request = await requestRepo.GetByIdAsync(model.BloodRequestId);
            if (request == null || request.DeletedTime != null)
                return new ApiErrorResult<object>("Blood request not found.");

            // ✅ Điều kiện 1: Người hiến không được là người yêu cầu máu
            if (user.Id == request.RequestedById)
                return new ApiErrorResult<object>("User cannot donate to their own blood request.");

            // ✅ Điều kiện 2: Chỉ cho phép hiến máu cho yêu cầu FromDonor
            if (request.RequestSource != "FromDonor")
                return new ApiErrorResult<object>("Donations can only be created for requests from donors.");

            // ✅ Điều kiện 3: Kiểm tra nhóm máu và component có tương thích
            var compatibility = await _unitOfWork.GetRepository<BloodCompatibility>().Entities
                .FirstOrDefaultAsync(x =>
                    x.DonorBloodGroupId == user.BloodGroupId &&
                    x.RecipientBloodGroupId == request.BloodGroupId &&
                    x.BloodComponent == request.BloodComponent &&
                    x.IsCompatible);

            if (compatibility == null)
                return new ApiErrorResult<object>("Blood group or component is not compatible.");

            // ✅ Điều kiện 4: Kiểm tra ngày hiến máu hợp lệ
            var now = DateTime.Now;
            var available = await availabilityRepo.Entities
                .Where(x => x.UserId == user.Id && x.IsActive && x.AvailableFrom <= now && x.AvailableTo >= now)
                .FirstOrDefaultAsync();

            if (available == null && user.LastDonationDate.HasValue)
            {
                var nextAllowedDate = user.LastDonationDate.Value.AddDays(90);
                if (now < nextAllowedDate)
                {
                    return new ApiErrorResult<object>($"User must wait until {nextAllowedDate:dd/MM/yyyy} to donate blood again (90 days interval required).");
                }
            }

            // ✅ Tạo bản ghi Donation
            var donation = new Donation
            {
                UserId = model.UserId,
                BloodRequestId = model.BloodRequestId,
                Quantity = model.Quantity,
                DonationDate = model.DonationDate,
                Notes = model.Notes,
                CreatedTime = DateTime.Now,
                CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0")
            };

            await donationRepo.InsertAsync(donation);
            await _unitOfWork.SaveAsync();

            // ✅ Cập nhật trạng thái BloodRequest
            var totalDonated = await donationRepo.Entities
                .Where(d => d.BloodRequestId == request.Id)
                .SumAsync(d => d.Quantity);

            if (totalDonated >= request.Quantity)
            {
                request.Status = BloodRequestStatus.Fulfilled;
                request.FulfilledDate = DateTime.Now;
            }
            else if (totalDonated > 0)
            {
                request.Status = BloodRequestStatus.PartiallyFulfilled;
            }

            request.LastUpdatedTime = DateTime.Now;
            request.LastUpdatedBy = donation.CreatedBy;
            requestRepo.Update(request);

            // ✅ Cập nhật LastDonationDate của user
            user.LastDonationDate = model.DonationDate;
            user.LastUpdatedTime = DateTime.Now;
            user.LastUpdatedBy = donation.CreatedBy;
            userRepo.Update(user);

            // ✅ Tạo mới hoặc cập nhật DonorAvailability
            if (available == null)
            {
                var newAvailability = new DonorAvailability
                {
                    UserId = user.Id,
                    AvailableFrom = model.DonationDate.AddDays(90),
                    AvailableTo = model.DonationDate.AddDays(180),
                    IsActive = true,
                    CreatedBy = donation.CreatedBy,
                    CreatedTime = DateTime.Now
                };
                await availabilityRepo.InsertAsync(newAvailability);
            }
            else
            {
                available.AvailableFrom = model.DonationDate.AddDays(90);
                available.AvailableTo = model.DonationDate.AddDays(180);
                available.LastUpdatedBy = donation.CreatedBy;
                available.LastUpdatedTime = DateTime.Now;
                availabilityRepo.Update(available);
            }

            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Donation created successfully.");
        }


        public async Task<ApiResult<object>> UpdateAsync(int id, UpdateDonationModelView model)
        {
            var donationRepo = _unitOfWork.GetRepository<Donation>();
            var entity = await donationRepo.GetByIdAsync(id);

            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Donation not found");

            var userId = model.UserId ?? entity.UserId;
            var requestId = model.BloodRequestId ?? entity.BloodRequestId;
            var donationDate = model.DonationDate ?? entity.DonationDate;

            var userRepo = _unitOfWork.GetRepository<ApplicationUser>();
            var requestRepo = _unitOfWork.GetRepository<BloodRequest>();
            var availabilityRepo = _unitOfWork.GetRepository<DonorAvailability>();

            var user = await userRepo.GetByIdAsync(userId);
            if (user == null || user.DeletedTime.HasValue)
                return new ApiErrorResult<object>("User not found.");

            var request = await requestRepo.GetByIdAsync(requestId);
            if (request == null || request.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Blood request not found.");

            if (user.Id == request.RequestedById)
                return new ApiErrorResult<object>("User cannot donate to their own blood request.");

            if (request.RequestSource != "FromDonor")
                return new ApiErrorResult<object>("Donations can only be created for requests from donors.");

            var compatibility = await _unitOfWork.GetRepository<BloodCompatibility>().Entities
                .FirstOrDefaultAsync(x =>
                    x.DonorBloodGroupId == user.BloodGroupId &&
                    x.RecipientBloodGroupId == request.BloodGroupId &&
                    x.BloodComponent == request.BloodComponent &&
                    x.IsCompatible);

            if (compatibility == null)
                return new ApiErrorResult<object>("Blood group or component is not compatible.");

            var now = DateTime.Now;
            var availability = await availabilityRepo.Entities
                .Where(x => x.UserId == user.Id && x.IsActive && x.AvailableFrom <= now && x.AvailableTo >= now)
                .FirstOrDefaultAsync();

            if (availability == null)
            {
                if (user.LastDonationDate.HasValue)
                {
                    var nextAllowedDate = user.LastDonationDate.Value.AddDays(90);
                    if (now < nextAllowedDate)
                    {
                        return new ApiErrorResult<object>($"User must wait until {nextAllowedDate:dd/MM/yyyy} to donate blood again (90 days interval required).");
                    }
                }
            }

            // Cập nhật thông tin
            entity.UserId = userId;
            entity.BloodRequestId = requestId;
            entity.DonationDate = donationDate;

            if (model.Quantity.HasValue)
                entity.Quantity = model.Quantity.Value;

            if (!string.IsNullOrWhiteSpace(model.Notes))
                entity.Notes = model.Notes;

            entity.LastUpdatedTime = DateTime.Now;
            entity.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            donationRepo.Update(entity);

            // Cập nhật LastDonationDate của user
            user.LastDonationDate = donationDate;
            user.LastUpdatedTime = DateTime.Now;
            user.LastUpdatedBy = entity.LastUpdatedBy;
            userRepo.Update(user);

            // Tạo mới hoặc cập nhật DonorAvailability
            if (availability == null)
            {
                var newAvailability = new DonorAvailability
                {
                    UserId = user.Id,
                    AvailableFrom = donationDate.AddDays(90),
                    AvailableTo = donationDate.AddDays(180),
                    IsActive = true,
                    CreatedBy = entity.LastUpdatedBy,
                    CreatedTime = DateTime.Now
                };
                await availabilityRepo.InsertAsync(newAvailability);
            }
            else
            {
                availability.AvailableFrom = donationDate.AddDays(90);
                availability.AvailableTo = donationDate.AddDays(180);
                availability.LastUpdatedBy = entity.LastUpdatedBy;
                availability.LastUpdatedTime = DateTime.Now;
                availabilityRepo.Update(availability);
            }

            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Donation updated successfully");
        }



        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.GetRepository<Donation>().GetByIdAsync(id);
            if (entity == null || entity.DeletedTime.HasValue)
                return new ApiErrorResult<object>("Donation not found");

            entity.DeletedTime = DateTime.Now;
            entity.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            _unitOfWork.GetRepository<Donation>().Update(entity);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Donation deleted successfully");
        }

    }
}
