using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.DonorProfileViews;
using Blood.Repositories.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Blood.Services.Service
{
    public class DonorProfileService : IDonorProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public DonorProfileService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResult<BasePaginatedList<DonorProfileModelView>>> GetAllDonorProfileAsync(
            int pageNumber, int pageSize, int? id, int? userId, int? bloodTypeId,
            string? healthStatus, bool? isAvailable)
        {
            IQueryable<DonorProfile> donorProfileQuery = _unitOfWork.GetRepository<DonorProfile>().Entities
                .Include(dp => dp.User)
                .Include(dp => dp.BloodType)
                .AsNoTracking()
                .OrderByDescending(dp => dp.CreatedTime)
                .Where(dp => !dp.DeletedTime.HasValue);

            if (id != null)
                donorProfileQuery = donorProfileQuery.Where(dp => dp.Id == id);

            if (userId != null)
                donorProfileQuery = donorProfileQuery.Where(dp => dp.UserId == userId);

            if (bloodTypeId != null)
                donorProfileQuery = donorProfileQuery.Where(dp => dp.BloodTypeId == bloodTypeId);

            if (!string.IsNullOrWhiteSpace(healthStatus))
                donorProfileQuery = donorProfileQuery.Where(dp => dp.HealthStatus.Contains(healthStatus));

            if (isAvailable != null)
                donorProfileQuery = donorProfileQuery.Where(dp => dp.IsAvailable == isAvailable);

            donorProfileQuery = donorProfileQuery.OrderByDescending(dp => dp.CreatedTime);

            int totalCount = await donorProfileQuery.CountAsync();

            List<DonorProfile> paginatedDonorProfiles = await donorProfileQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            List<DonorProfileModelView> donorProfileModelViews = _mapper.Map<List<DonorProfileModelView>>(paginatedDonorProfiles);
            var result = new BasePaginatedList<DonorProfileModelView>(donorProfileModelViews, totalCount, pageNumber, pageSize);

            return new ApiSuccessResult<BasePaginatedList<DonorProfileModelView>>(result);
        }

        public async Task<ApiResult<object>> AddDonorProfileAsync(CreateDonorProfileModelView model)
        {
            // Check if user exists
            var existedUser = await _unitOfWork.GetRepository<ApplicationUser>()
                .Entities
                .FirstOrDefaultAsync(user => user.Id == model.UserId && !user.DeletedTime.HasValue);

            if (existedUser == null)
            {
                return new ApiErrorResult<object>("User not found or has been deleted.");
            }

            // Check if donor profile already exists for this user
            var existedDonorProfile = await _unitOfWork.GetRepository<DonorProfile>()
                .Entities
                .FirstOrDefaultAsync(dp => dp.UserId == model.UserId && !dp.DeletedTime.HasValue);

            if (existedDonorProfile != null)
            {
                return new ApiErrorResult<object>("Donor profile already exists for this user.");
            }

            // Check if blood type exists
            var existedBloodType = await _unitOfWork.GetRepository<BloodType>()
                .Entities
                .FirstOrDefaultAsync(bt => bt.Id == model.BloodTypeId && !bt.DeletedTime.HasValue);

            if (existedBloodType == null)
            {
                return new ApiErrorResult<object>("Blood type not found or has been deleted.");
            }

            DonorProfile newDonorProfile = _mapper.Map<DonorProfile>(model);

            newDonorProfile.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            newDonorProfile.CreatedTime = DateTime.Now;

            await _unitOfWork.GetRepository<DonorProfile>().InsertAsync(newDonorProfile);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Donor profile added successfully.");
        }

        public async Task<ApiResult<object>> UpdateDonorProfileAsync(int id, UpdateDonorProfileModelView model)
        {
            if (id == null)
            {
                return new ApiErrorResult<object>("Please provide a valid Donor Profile ID.");
            }

            var existingDonorProfile = await _unitOfWork.GetRepository<DonorProfile>().Entities
                .FirstOrDefaultAsync(dp => dp.Id == id && !dp.DeletedTime.HasValue);

            if (existingDonorProfile == null)
            {
                return new ApiErrorResult<object>("The Donor Profile cannot be found or has been deleted!");
            }

            bool isUpdated = false;

            // Check blood type if provided
            if (model.BloodTypeId != null && model.BloodTypeId != existingDonorProfile.BloodTypeId)
            {
                var existedBloodType = await _unitOfWork.GetRepository<BloodType>()
                    .Entities
                    .FirstOrDefaultAsync(bt => bt.Id == model.BloodTypeId && !bt.DeletedTime.HasValue);

                if (existedBloodType == null)
                {
                    return new ApiErrorResult<object>("Blood type not found or has been deleted.");
                }

                existingDonorProfile.BloodTypeId = model.BloodTypeId.Value;
                isUpdated = true;
            }

            if (model.Weight != null && model.Weight != existingDonorProfile.Weight)
            {
                existingDonorProfile.Weight = model.Weight;
                isUpdated = true;
            }

            if (model.Height != null && model.Height != existingDonorProfile.Height)
            {
                existingDonorProfile.Height = model.Height;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(model.HealthStatus) && model.HealthStatus != existingDonorProfile.HealthStatus)
            {
                existingDonorProfile.HealthStatus = model.HealthStatus;
                isUpdated = true;
            }

            if (model.LastDonationDate != null && model.LastDonationDate != existingDonorProfile.LastDonationDate)
            {
                existingDonorProfile.LastDonationDate = model.LastDonationDate;
                isUpdated = true;
            }

            if (model.NextAvailableDate != null && model.NextAvailableDate != existingDonorProfile.NextAvailableDate)
            {
                existingDonorProfile.NextAvailableDate = model.NextAvailableDate;
                isUpdated = true;
            }

            if (model.DonationCount != null && model.DonationCount != existingDonorProfile.DonationCount)
            {
                existingDonorProfile.DonationCount = model.DonationCount.Value;
                isUpdated = true;
            }

            if (model.IsAvailable != null && model.IsAvailable != existingDonorProfile.IsAvailable)
            {
                existingDonorProfile.IsAvailable = model.IsAvailable.Value;
                isUpdated = true;
            }

            if (model.IsEmergencyAvailable != null && model.IsEmergencyAvailable != existingDonorProfile.IsEmergencyAvailable)
            {
                existingDonorProfile.IsEmergencyAvailable = model.IsEmergencyAvailable.Value;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(model.PreferredDonationType) && model.PreferredDonationType != existingDonorProfile.PreferredDonationType)
            {
                existingDonorProfile.PreferredDonationType = model.PreferredDonationType;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(model.MedicalHistory) && model.MedicalHistory != existingDonorProfile.MedicalHistory)
            {
                existingDonorProfile.MedicalHistory = model.MedicalHistory;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(model.Notes) && model.Notes != existingDonorProfile.Notes)
            {
                existingDonorProfile.Notes = model.Notes;
                isUpdated = true;
            }

            if (isUpdated)
            {
                existingDonorProfile.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
                existingDonorProfile.LastUpdatedTime = DateTime.Now;

                await _unitOfWork.GetRepository<DonorProfile>().UpdateAsync(existingDonorProfile);
                await _unitOfWork.SaveAsync();
            }

            return new ApiSuccessResult<object>("Donor profile successfully updated.");
        }

        public async Task<ApiResult<object>> DeleteDonorProfileAsync(int id)
        {
            if (id == null)
            {
                return new ApiErrorResult<object>("Please provide a valid Donor Profile ID.");
            }

            var existingDonorProfile = await _unitOfWork.GetRepository<DonorProfile>().Entities
                .FirstOrDefaultAsync(dp => dp.Id == id && !dp.DeletedTime.HasValue);

            if (existingDonorProfile == null)
            {
                return new ApiErrorResult<object>("The Donor Profile cannot be found or has been deleted!");
            }

            existingDonorProfile.DeletedTime = DateTime.Now;
            existingDonorProfile.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            await _unitOfWork.GetRepository<DonorProfile>().UpdateAsync(existingDonorProfile);
            await _unitOfWork.SaveAsync();

            return new ApiSuccessResult<object>("Donor profile successfully deleted.");
        }

        public async Task<ApiResult<DonorProfileModelView>> GetDonorProfileByIdAsync(int id)
        {
            if (id == null)
            {
                return new ApiErrorResult<DonorProfileModelView>("Please provide a valid Donor Profile ID.");
            }

            var donorProfileEntity = await _unitOfWork.GetRepository<DonorProfile>().Entities
                .Include(dp => dp.User)
                .Include(dp => dp.BloodType)
                .FirstOrDefaultAsync(dp => dp.Id == id && !dp.DeletedTime.HasValue);

            if (donorProfileEntity == null)
            {
                return new ApiErrorResult<DonorProfileModelView>("Donor profile does not exist.");
            }

            DonorProfileModelView donorProfileModelView = _mapper.Map<DonorProfileModelView>(donorProfileEntity);
            return new ApiSuccessResult<DonorProfileModelView>(donorProfileModelView);
        }

        public async Task<ApiResult<DonorProfileModelView>> GetDonorProfileByUserIdAsync(int userId)
        {
            if (userId == null)
            {
                return new ApiErrorResult<DonorProfileModelView>("Please provide a valid User ID.");
            }

            var donorProfileEntity = await _unitOfWork.GetRepository<DonorProfile>().Entities
                .Include(dp => dp.User)
                .Include(dp => dp.BloodType)
                .FirstOrDefaultAsync(dp => dp.UserId == userId && !dp.DeletedTime.HasValue);

            if (donorProfileEntity == null)
            {
                return new ApiErrorResult<DonorProfileModelView>("Donor profile does not exist for this user.");
            }

            DonorProfileModelView donorProfileModelView = _mapper.Map<DonorProfileModelView>(donorProfileEntity);
            return new ApiSuccessResult<DonorProfileModelView>(donorProfileModelView);
        }
    }
}
