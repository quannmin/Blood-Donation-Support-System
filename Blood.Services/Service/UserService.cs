using AutoMapper;
using Blood.Contract.Repositories.Entity;
using Blood.Contract.Repositories.Interface;
using Blood.Contract.Services.Interface;
using Blood.Core;
using Blood.Core.APIResponse;
using Blood.Core.Utils;
using Blood.Core.Utils.Firebase;
using Blood.ModelViews.AuthModelViews.Request;
using Blood.ModelViews.AuthModelViews.Response;
using Blood.ModelViews.BloodGroupModelViews;
using Blood.ModelViews.RoleModelViews;
using Blood.ModelViews.UserModelViews;
using Blood.ModelViews.UserModelViews.Request;
using Blood.ModelViews.UserModelViews.Response;
using Blood.Repositories.Entity;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Blood.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public UserService(
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            RoleManager<ApplicationRole> roleManager,
            IMemoryCache memoryCache) // <- thêm dòng này
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _memoryCache = memoryCache; // <- gán ở đây
        }

        public async Task<ApiResult<UserLoginResponseModel>> ConfirmUserRegister(ConfirmUserRegisterRequest request)
        {
            // Check existed email
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            // Confirm code 
            var result = await _userManager.ConfirmEmailAsync(existingUser, request.Code);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Confirm email unsuccessfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            existingUser.Status = true;
            var rs = await _userManager.UpdateAsync(existingUser);

            if (!rs.Succeeded)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Update unsuccessfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            var refreshTokenData = GenerateRefreshToken();
            var accessTokenData = await GenerateAccessTokenAsync(existingUser);
            existingUser.RefreshToken = refreshTokenData.Item1;
            existingUser.RefreshTokenExpiryTime = refreshTokenData.Item2;

            await _userManager.UpdateAsync(existingUser);
            var response = _mapper.Map<UserLoginResponseModel>(existingUser);
            response.AccessToken = accessTokenData.Item1;
            response.AccessTokenExpiredTime = accessTokenData.Item2;
            response.RefreshToken = refreshTokenData.Item1;
            response.RefreshTokenExpiryTime = refreshTokenData.Item2;
            response.FullName = existingUser.FullName ?? "Unknown";

            return new ApiSuccessResult<UserLoginResponseModel>(response, "Register successfully.");
        }

        private (string, DateTime) GenerateRefreshToken()
        {
            var expiredTime = DateTime.Now.AddMinutes(Blood.Core.Utils.TimeHelper.DURATION_REFRESH_TOKEN_TIME);
            var refreshToken = "";
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                refreshToken = Convert.ToBase64String(random);
            }
            return (refreshToken, expiredTime);
        }
        private async Task<(string, DateTime)> GenerateAccessTokenAsync(ApplicationUser user)
        {
            var expiredTime = DateTime.Now.AddMinutes(Blood.Core.Utils.TimeHelper.DURATION_ACCESS_TOKEN_TIME);
            var authClaims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim("Role", role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: expiredTime,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return (accessToken, expiredTime);
        }

        public async Task<ApiResult<object>> CreateEmployee(CreateEmployeeRequest request)
        {
            // Check existed username
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (existingUser != null)
            {
                return new ApiErrorResult<object>("Username is existed.", System.Net.HttpStatusCode.Conflict);
            }
            // Check existed email
            var existingEmail = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingEmail != null)
            {
                return new ApiErrorResult<object>("Email is existed.", System.Net.HttpStatusCode.Conflict);
            }

            // Createe user use mapper
            var user = _mapper.Map<ApplicationUser>(request);
            user.Status = true;

            if (request.AvatarUrl != null)
            {
                user.AvatarUrl = await Blood.Core.Utils.Firebase.ImageHelper.Upload(request.AvatarUrl);
            }

            user.CreatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new ApiErrorResult<object>("Create user unsuccessfully.", result.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest);
            // Find role
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId);
            if (role == null)
                return new ApiErrorResult<object>("Default role not found.", HttpStatusCode.NotFound);

            await _userManager.AddToRoleAsync(user, role.Name);

            return new ApiSuccessResult<object>("Create user successfully.");
        }

        public async Task<ApiResult<object>> DeleteEmployee(DeleteUserRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            // Delete user
            existingUser.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            existingUser.DeletedTime = DateTime.Now;
            var result = await _userManager.UpdateAsync(existingUser);
            // Return to client
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Delete user unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Delete user successfully.");
        }

        public async Task<ApiResult<object>> DeleteUser(DeleteUserRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            // Delete user
            existingUser.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");
            existingUser.DeletedTime = DateTime.Now;
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Delete usere unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Delete user successfully.");
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Mã 6 chữ số
        }

        public async Task<ApiResult<object>> EmployeeForgotPassword(ForgotPasswordRequest request)
        {
            var email = request.Email;
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email không tồn tại.", System.Net.HttpStatusCode.NotFound);
            }

            var code = GenerateVerificationCode();

            // Lưu code tạm thời - ví dụ bằng memory cache / redis
            _memoryCache.Set($"ResetPasswordCode_{email}", code, TimeSpan.FromMinutes(10));

            // Load file HTML và thay {{VerifyCode}} bằng mã code
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "SendCode.html");
            if (!File.Exists(path))
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");

            string contentCustomer = File.ReadAllText(path);
            contentCustomer = contentCustomer.Replace("{{OTP}}", code); // chỉ là mã code, không phải link

            var sendMailResult = DoingMail.SendMail("Blood Donation", "Mã xác thực thay đổi mật khẩu", contentCustomer, email);
            if (!sendMailResult)
                return new ApiErrorResult<object>("Gửi email thất bại. Vui lòng thử lại sau");

            return new ApiSuccessResult<object>("Vui lòng kiểm tra email để lấy mã xác thực.");
        }

        public async Task<ApiResult<EmployeeLoginResponseModel>> EmployeeLogin(EmployeeLoginRequestModel request)
        {
            // Check valid username
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Username or password is not correct.", System.Net.HttpStatusCode.NotFound);
            }
            if (existingUser.DeletedBy != null)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Username or password is not correct.", System.Net.HttpStatusCode.NotFound);
            }
            // Check valid password
            var validPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!validPassword)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Username or password is not correct.", System.Net.HttpStatusCode.NotFound);
            }
            // Check valid role doctor or admin
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (!userRoles.Contains("Doctor") && !userRoles.Contains("Admin"))
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Username or password is not correct.", System.Net.HttpStatusCode.NotFound);
            }
            if (existingUser.Status == false)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("You cannot access system.", System.Net.HttpStatusCode.NotFound);

            }

            // Generate refresh token
            var refreshTokenData = GenerateRefreshToken();
            var accessTokenData = await GenerateAccessTokenAsync(existingUser);
            existingUser.RefreshToken = refreshTokenData.Item1;
            existingUser.RefreshTokenExpiryTime = refreshTokenData.Item2;
            await _userManager.UpdateAsync(existingUser);
            // Response to client
            var response = _mapper.Map<EmployeeLoginResponseModel>(existingUser);
            response.AccessToken = accessTokenData.Item1;
            response.AccessTokenExpiredTime = accessTokenData.Item2;
            response.FullName = existingUser.FullName ?? "Unknown";
            // Take role
            var roles = await _userManager.GetRolesAsync(existingUser);
            response.Roles = roles.ToList();
            return new ApiSuccessResult<EmployeeLoginResponseModel>(response, "Login successfully.");
        }

        public async Task<ApiResult<object>> EmployeeResetPassword(ResetPasswordRequestModel request)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email không tồn tại.", System.Net.HttpStatusCode.NotFound);
            }

            // Kiểm tra role hợp lệ
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (!userRoles.Contains("Admin") && !userRoles.Contains("Doctor"))
            {
                return new ApiErrorResult<object>("Email không tồn tại.", System.Net.HttpStatusCode.NotFound);
            }

            // Kiểm tra mã xác thực
            var cacheKey = $"ResetPasswordCode_{request.Email}";
            if (!_memoryCache.TryGetValue(cacheKey, out string correctCode) || correctCode != request.Code)
            {
                return new ApiErrorResult<object>("Mã xác thực không hợp lệ hoặc đã hết hạn", System.Net.HttpStatusCode.BadRequest);
            }

            // Xóa mã sau khi sử dụng
            _memoryCache.Remove(cacheKey);

            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            var result = await _userManager.ResetPasswordAsync(existingUser, token, request.Password);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Đặt lại mật khẩu thất bại", result.Errors.Select(x => x.Description).ToList());
            }

            return new ApiSuccessResult<object>("Đặt lại mật khẩu thành công.");
        }

        public async Task<ApiResult<object>> ForgotPassword(ForgotPasswordRequest request)
        {
            var email = request.Email.Trim().ToLower();
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return new ApiErrorResult<object>("Email không tồn tại.", HttpStatusCode.NotFound);

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new ApiErrorResult<object>("Email chưa được xác nhận.", HttpStatusCode.BadRequest);

            // Tạo mã 6 số
            var code = GenerateVerificationCode();

            // Lưu vào cache với key: ResetPassword:{Email}
            _memoryCache.Set($"ResetPassword:{email}", code, TimeSpan.FromMinutes(10));

            // Gửi email
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "SendCodeCustomer.html");
            if (!File.Exists(path))
                return new ApiErrorResult<object>("Lỗi hệ thống, thử lại sau.", HttpStatusCode.InternalServerError);

            string emailContent = File.ReadAllText(path).Replace("{{OTP}}", code);
            var sendResult = DoingMail.SendMail("Blood Donation", "Mã xác minh đặt lại mật khẩu", emailContent, email);
            if (!sendResult)
                return new ApiErrorResult<object>("Lỗi khi gửi email.", HttpStatusCode.InternalServerError);

            return new ApiSuccessResult<object>("Mã xác minh đã được gửi đến email của bạn.");
        }

        public async Task<ApiResult<List<EmployeeResponseModel>>> GetAllDoctor()
        {
            var ownerRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "Doctor");


            // Filter users

            var doctorUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == ownerRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = await _userManager.Users
                .Where(u => doctorUserIds.Contains(u.Id) && u.DeletedBy == null)
                .OrderByDescending(x => x.LastUpdatedTime)
                .ToListAsync();



            var items = users.Select(user => new EmployeeResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                BloodGroup = _mapper.Map<BloodGroupModelView>(user.BloodGroup),

                Status = user.Status,
                Role = new RoleModelView
                {
                    Id = ownerRole.Id,
                    Name = ownerRole.Name
                }
            }).ToList();

            // return to client
            return new ApiSuccessResult<List<EmployeeResponseModel>>(items);
        }

        public async Task<ApiResult<List<EmployeeResponseModel>>> GetAllEmployee()
        {
            // Lấy danh sách Role có tên DOCTOR hoặc ADMIN
            var AdminRoles = await _roleManager.Roles
                .Where(r => r.Name == "Admin" || r.Name == "Doctor")
                .ToListAsync();

            if (AdminRoles == null || !AdminRoles.Any())
            {
                return new ApiSuccessResult<List<EmployeeResponseModel>>(new List<EmployeeResponseModel>());
            }

            // Lấy danh sách RoleId tương ứng
            var roleIds = AdminRoles.Select(r => r.Id).ToList();

            // Lấy danh sách UserId của những người có RoleId nằm trong danh sách roleIds
            var doctorAdminUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => roleIds.Contains(ur.RoleId))
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = await _userManager.Users
                .Where(u => doctorAdminUserIds.Contains(u.Id) && u.DeletedBy == null)
                .OrderByDescending(ur => ur.LastUpdatedTime)
                .ToListAsync();

            // Trả về danh sách nhân viên
            var items = users.Select(user => new EmployeeResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                BloodGroup = _mapper.Map<BloodGroupModelView>(user.BloodGroup),

                Status = user.Status,
                // Lấy role phù hợp của user
                Role = new ModelViews.RoleModelViews.RoleModelView()
                {
                    Id = (int)AdminRoles.FirstOrDefault(r => r.Id ==
                         _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                         .Where(ur => ur.UserId == user.Id)
                         .Select(ur => ur.RoleId)
                         .FirstOrDefault()
                    )?.Id,
                    Name = AdminRoles.FirstOrDefault(r => r.Id ==
                         _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                         .Where(ur => ur.UserId == user.Id)
                         .Select(ur => ur.RoleId)
                         .FirstOrDefault()
                    )?.Name
                }
            }).ToList();

            return new ApiSuccessResult<List<EmployeeResponseModel>>(items);
        }

        public async Task<ApiResult<List<UserResponseModel>>> GetAllUser()
        {
            var userRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "User");


            // Filter users

            var UserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == userRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = await _userManager.Users
                .Where(u => UserIds.Contains(u.Id) && u.DeletedBy == null)
                .OrderByDescending(x => x.LastUpdatedTime)
                .ToListAsync();



            var items = users.Select(x => new UserResponseModel
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                AvatarUrl = x.AvatarUrl,
                PhoneNumber = x.PhoneNumber,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                BloodGroup = _mapper.Map<BloodGroupModelView>(x.BloodGroup),

                Status = x.Status,
                Address = x.Address,
                LastDonationDate = x.LastDonationDate,

            }).ToList();

            // return to client
            return new ApiSuccessResult<List<UserResponseModel>>(items);
        }

        public async Task<ApiResult<BasePaginatedList<EmployeeResponseModel>>> GetDoctorPagination(string? Email, int? PageIndex, int? PageSize)
        {
            var ownerRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "Doctor");
            if (ownerRole == null)
            {
                return new ApiErrorResult<BasePaginatedList<EmployeeResponseModel>>("Role Doctor not found");
            }

            var ownerUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == ownerRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            var usersQuery = _userManager.Users
                .OrderByDescending(r => r.LastUpdatedTime)
                .Where(u => ownerUserIds.Contains(u.Id) && u.DeletedBy == null);

            // Apply search
            if (!string.IsNullOrWhiteSpace(Email))
            {
                var searchLower = Email.ToLower();
                usersQuery = usersQuery.Where(x =>
                    x.Email.ToLower().Contains(searchLower));
            }

            // Pagination
            var currentPage = PageIndex ?? 1;
            var pageSize = PageSize ?? SystemConstant.PAGE_SIZE;
            var total = await usersQuery.CountAsync();
            var pagedUsers = await usersQuery
                .OrderBy(u => u.FullName) // Optional: consistent order
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = pagedUsers.Select(user => new EmployeeResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                BloodGroup = _mapper.Map<BloodGroupModelView>(user.BloodGroup),

                Status = user.Status,
                Role = new RoleModelView
                {
                    Id = ownerRole.Id,
                    Name = ownerRole.Name
                }
            }).ToList();

            var response = new BasePaginatedList<EmployeeResponseModel>(items, total, currentPage, pageSize);
            // return to client
            return new ApiSuccessResult<BasePaginatedList<EmployeeResponseModel>>(response);
        }

        public async Task<ApiResult<EmployeeResponseModel>> GetEmployeeById(int Id)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<EmployeeResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (existingUser.DeletedBy != null)
            {
                return new ApiErrorResult<EmployeeResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);

            }
            // Check role Admin or Doctor
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (!userRoles.Contains("Doctor") && !userRoles.Contains("Admin"))
            {
                return new ApiErrorResult<EmployeeResponseModel>("User is not valid.", System.Net.HttpStatusCode.NotFound);
            }

            // Response to client
            var response = _mapper.Map<EmployeeResponseModel>(existingUser);

            response.BloodGroup = _mapper.Map<BloodGroupModelView>(existingUser.BloodGroup);

            return new ApiSuccessResult<EmployeeResponseModel>(response);
        }

        public async Task<ApiResult<UserResponseModel>> GetUserById(int Id)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<UserResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            // Check isUser
            var isValidUser = await _userManager.GetRolesAsync(existingUser);
            foreach (var item in isValidUser)
            {
                if (item != "User")
                {
                    return new ApiErrorResult<UserResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);
                }
            }
            // Response to client
            var response = _mapper.Map<UserResponseModel>(existingUser);

            response.BloodGroup = _mapper.Map<BloodGroupModelView>(existingUser.BloodGroup);

            return new ApiSuccessResult<UserResponseModel>(response);
        }

        public async Task<ApiResult<BasePaginatedList<UserResponseModel>>> GetUserPagination(string? Email, int? PageIndex, int? PageSize)
        {
            // all user
            var userRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "User");


            // Filter users

            var doctorUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == userRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = _userManager.Users
                .OrderByDescending(r => r.LastUpdatedTime)
                .Where(u => doctorUserIds.Contains(u.Id) && u.DeletedBy == null);
            // filter by search 
            if (!string.IsNullOrEmpty(Email))
            {
                users = users.Where(x => x.Email.ToLower().Contains(Email.ToLower()));
            }
            // paging
            var currentPage = PageIndex ?? 1;
            var pageSize = PageSize ?? SystemConstant.PAGE_SIZE;
            var total = users.Count();
            var data = await users.Skip((currentPage - 1) * currentPage).Take(pageSize).ToListAsync();
            // calculate total page

            var items = data.Select(x => new UserResponseModel
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                AvatarUrl = x.AvatarUrl,
                PhoneNumber = x.PhoneNumber,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                BloodGroup = _mapper.Map<BloodGroupModelView>(x.BloodGroup),

                Status = x.Status,
                Address = x.Address,
                LastDonationDate = x.LastDonationDate,

            }).ToList();

            var response = new BasePaginatedList<UserResponseModel>(items, total, currentPage, pageSize);
            // return to client
            return new ApiSuccessResult<BasePaginatedList<UserResponseModel>>(response);
        }

        public async Task<ApiResult<DashboardUserCreateResponse>> GetUsersByCreateTime()
        {
            var now = DateTime.Now;

            var users = await _userManager.Users.ToListAsync(); // Lấy tất cả users để xử lý
            var usersWithRoleUser = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("User")) // Kiểm tra user có role "User"
                {
                    usersWithRoleUser.Add(user);
                }
            }

            var usersY = usersWithRoleUser.Where(u => u.CreatedTime.Year == now.Year).ToList();
            var usersM = usersWithRoleUser.Where(u => u.CreatedTime.Month == now.Month).ToList();
            var usersD = usersWithRoleUser.Where(u => u.CreatedTime.Date == now.Date).ToList();

            var res = new DashboardUserCreateResponse()
            {
                InDay = usersD.Count,
                InMonth = usersM.Count,
                InYear = usersY.Count,
            };

            return new ApiSuccessResult<DashboardUserCreateResponse>(res);
        }

        public async Task<ApiResult<EmployeeLoginResponseModel>> RefreshToken(NewRefreshTokenRequestModel request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            // Check refresh token
            if (existingUser.RefreshToken != request.RefreshToken)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Refresh token is not correct.", System.Net.HttpStatusCode.BadRequest);
            }
            // Check expired time
            if (existingUser.RefreshTokenExpiryTime < DateTime.Now)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Refresh token is expired.", System.Net.HttpStatusCode.BadRequest);
            }
            // Generate new refresh token
            var refreshTokenData = GenerateRefreshToken();
            var accessTokenData = await GenerateAccessTokenAsync(existingUser);
            existingUser.RefreshToken = refreshTokenData.Item1;
            existingUser.RefreshTokenExpiryTime = refreshTokenData.Item2;
            await _userManager.UpdateAsync(existingUser);
            // Response to client
            var response = _mapper.Map<EmployeeLoginResponseModel>(existingUser);
            response.AccessToken = accessTokenData.Item1;
            response.AccessTokenExpiredTime = accessTokenData.Item2;
            response.FullName = existingUser.FullName ?? "Unknown";

            // Take role
            var roles = await _userManager.GetRolesAsync(existingUser);
            response.Roles = roles.ToList();
            return new ApiSuccessResult<EmployeeLoginResponseModel>(response, "Refresh token successfully.");
        }

        public async Task<ApiResult<object>> RegisterDoctor(UserRegisterRequestModel request)
        {
            return await RegisterWithRole(request, roleId: "2");
        }

        public async Task<ApiResult<object>> RegisterUser(UserRegisterRequestModel request)
        {
            return await RegisterWithRole(request, roleId: "3");
        }

        private async Task<string> _generateUsernameOfGuestAsync()
        {
            Random random = new Random();
            while (true)
            {
                var username = "USER_" + random.Next(0, 999999).ToString("D6");

                var userCheckExisted = await _userManager.FindByNameAsync(username);
                if (userCheckExisted == null)
                {
                    return username;
                }
            }
        }

        public async Task<ApiResult<object>> RegisterWithRole(UserRegisterRequestModel request, string roleId)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.Status == true);
            if (existingUser != null)
            {
                return new ApiErrorResult<object>("Email is existed.", System.Net.HttpStatusCode.BadRequest);
            }

            var existingUser2 = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.Status == false);
            ApplicationUser user;

            if (existingUser2 != null)
            {
                // Update user cũ thay vì xóa
                existingUser2.FullName = request.FullName;
                existingUser2.UserName = await _generateUsernameOfGuestAsync();
                existingUser2.PhoneNumber = request.PhoneNumber;
                existingUser2.Status = false;

                var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(existingUser2);
                var resetResult = await _userManager.ResetPasswordAsync(existingUser2, resetPassToken, request.Password);
                if (!resetResult.Succeeded)
                {
                    return new ApiErrorResult<object>("Reset mật khẩu không thành công.", resetResult.Errors.Select(x => x.Description).ToList());
                }

                // Xóa roles cũ (nếu có) và gán role mới
                var existingRoles = await _userManager.GetRolesAsync(existingUser2);
                if (existingRoles.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(existingUser2, existingRoles);
                }

                user = existingUser2;
            }
            else
            {
                user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = await _generateUsernameOfGuestAsync(),
                    FullName = request.FullName,
                    Status = false,
                    PhoneNumber = request.PhoneNumber
                };

                var createResult = await _userManager.CreateAsync(user, request.Password);
                if (!createResult.Succeeded)
                {
                    return new ApiErrorResult<object>("Register unsuccessfully.", createResult.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
                }
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return new ApiErrorResult<object>("Không tìm thấy vai trò.");
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!addRoleResult.Succeeded)
            {
                return new ApiErrorResult<object>("Không thể gán vai trò.", addRoleResult.Errors.Select(x => x.Description).ToList());
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "Welcome.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");
            }

            var content = File.ReadAllText(path);
            content = content.Replace("{{OTP}}", Uri.EscapeDataString(token));
            content = content.Replace("{{Name}}", user.Email);

            string roleMessage = roleId switch
            {
                "2" => $"Chúc mừng {user.Email} đã đăng ký thành công tài khoản bác sĩ trên Blood Donation.",
                "3" => $"Chúc mừng {user.Email} đã đăng ký thành công tài khoản người dùng trên Blood Donation.",
                _ => $"Chúc mừng {user.Email} đã đăng ký thành công tài khoản trên TeamUp."
            };
            content = content.Replace("{{RoleMessage}}", roleMessage);

            var resultSendMail = DoingMail.SendMail("Blood Donation", "Welcome", content, user.Email);
            if (!resultSendMail)
            {
                return new ApiErrorResult<object>("Cannot send email to " + request.Email);
            }

            return new ApiSuccessResult<object>("Please check your email to confirm.");
        }

        public async Task<ApiResult<object>> ResendOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ApiErrorResult<object>("Không tìm thấy người dùng với email này.");
            }

            if (user.Status == true)
            {
                return new ApiErrorResult<object>("Tài khoản đã được xác nhận, không cần gửi lại OTP.");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "Welcome.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");
            }

            var content = File.ReadAllText(path);
            content = content.Replace("{{OTP}}", Uri.EscapeDataString(token));
            content = content.Replace("{{Name}}", user.Email);

            string roleMessage = "Vui lòng xác nhận lại email để hoàn tất quá trình đăng ký tài khoản TeamUp.";
            content = content.Replace("{{RoleMessage}}", roleMessage);

            var sendResult = DoingMail.SendMail("Blood", "Xác nhận lại Email", content, user.Email);
            if (!sendResult)
            {
                return new ApiErrorResult<object>("Không thể gửi email xác nhận đến " + email);
            }

            return new ApiSuccessResult<object>("OTP đã được gửi lại thành công. Vui lòng kiểm tra email.");
        }

        public async Task<ApiResult<object>> ResetPassword(ResetPasswordRequestModel request)
        {
            var email = request.Email.Trim().ToLower();
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return new ApiErrorResult<object>("Email không tồn tại.", HttpStatusCode.NotFound);

            if (!_memoryCache.TryGetValue<string>($"ResetPassword:{email}", out var cachedCode))
                return new ApiErrorResult<object>("Mã xác minh đã hết hạn hoặc không hợp lệ.", HttpStatusCode.BadRequest);

            if (cachedCode != request.Code)
                return new ApiErrorResult<object>("Mã xác minh không đúng.", HttpStatusCode.BadRequest);

            if (request.Password != request.ConfirmPassword)
                return new ApiErrorResult<object>("Mật khẩu xác nhận không khớp.", HttpStatusCode.BadRequest);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new ApiErrorResult<object>("Đặt lại mật khẩu thất bại.", errors, HttpStatusCode.BadRequest);
            }

            // Xóa cache sau khi dùng
            _memoryCache.Remove($"ResetPassword:{email}");

            return new ApiSuccessResult<object>("Mật khẩu đã được thay đổi thành công.");
        }

        public async Task<ApiResult<object>> UpdateDoctorProfile(UpdateEmployeeProfileRequest request)
        {
            // Check if user exists
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            var existingImage = existingUser.AvatarUrl;

            // Update only non-null or non-empty fields
            if (!string.IsNullOrWhiteSpace(request.FullName))
                existingUser.FullName = request.FullName;

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                existingUser.PhoneNumber = request.PhoneNumber;

            if (request.DateOfBirth.HasValue)
                existingUser.DateOfBirth = request.DateOfBirth;

            if (!string.IsNullOrWhiteSpace(request.Gender))
                existingUser.Gender = request.Gender;

            if (request.BloodGroupId.HasValue)
                existingUser.BloodGroupId = request.BloodGroupId;

            if (request.AvatarUrl != null)
            {
                existingUser.AvatarUrl = await ImageHelper.Upload(request.AvatarUrl);
            }
            else
            {
                existingUser.AvatarUrl = existingImage;
            }

            existingUser.LastUpdatedTime = DateTime.Now;
            existingUser.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>(
                    "Update profile unsuccessfully",
                    result.Errors.Select(x => x.Description).ToList(),
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            return new ApiSuccessResult<object>("Update profile successfully.");

        }

        public async Task<ApiResult<object>> UpdateEmployeeStatus(UpdateUserStatusRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            // Update status
            existingUser.Status = request.Status;
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Update status unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Update status successfully.");
        }

        public async Task<ApiResult<object>> UpdateUserProfile(UpdateUserProfileRequest request)
        {
            // Check if user exists
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            var existingImage = existingUser.AvatarUrl;

            // Update only non-null or non-empty fields
            if (!string.IsNullOrWhiteSpace(request.FullName))
                existingUser.FullName = request.FullName;

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                existingUser.PhoneNumber = request.PhoneNumber;

            if (request.DateOfBirth.HasValue)
                existingUser.DateOfBirth = request.DateOfBirth;

            if (!string.IsNullOrWhiteSpace(request.Gender))
                existingUser.Gender = request.Gender;

            if (request.BloodGroupId.HasValue)
                existingUser.BloodGroupId = request.BloodGroupId;

            if (request.AvatarUrl != null)
            {
                existingUser.AvatarUrl = await ImageHelper.Upload(request.AvatarUrl);
            }
            else
            {
                existingUser.AvatarUrl = existingImage;
            }

            existingUser.LastUpdatedTime = DateTime.Now;
            existingUser.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value ?? "0");

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>(
                    "Update profile unsuccessfully",
                    result.Errors.Select(x => x.Description).ToList(),
                    System.Net.HttpStatusCode.BadRequest
                );
            }

            return new ApiSuccessResult<object>("Update profile successfully.");
        }

        public async Task<ApiResult<object>> UpdateUserStatus(UpdateUserStatusRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            // Update status
            existingUser.Status = request.Status;
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Update status unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Update status successfully.");
        }

        public async Task<ApiResult<UploadImageResponseModel>> UploadImage(UploadImageRequest request)
        {
            string res = await Blood.Core.Utils.Firebase.ImageHelper.Upload(request.Image);
            return new ApiSuccessResult<UploadImageResponseModel>(new UploadImageResponseModel { ImageUrl = res });
        }

        public async Task<ApiResult<UserLoginResponseModel>> UserLogin(UserLoginRequestModel request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email hoặc mật khẩu không đúng.");
            }
            if (existingUser.DeletedBy != null)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email hoặc mật khẩu không đúng.");
            }
            var validPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!validPassword)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email hoặc mật khẩu không đúng.");
            }
            var roles = await _userManager.GetRolesAsync(existingUser);

            var isConfirmed = await _userManager.IsEmailConfirmedAsync(existingUser);
            if (!isConfirmed)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email hoặc mật khẩu không đúng.");

            }

            if (existingUser.Status == false)
            {
                return new ApiErrorResult<UserLoginResponseModel>("You cannot access system.");

            }
            var refreshTokenData = GenerateRefreshToken();
            var accessTokenData = await GenerateAccessTokenAsync(existingUser);
            existingUser.RefreshToken = refreshTokenData.Item1;
            existingUser.RefreshTokenExpiryTime = refreshTokenData.Item2;

            await _userManager.UpdateAsync(existingUser);
            var response = _mapper.Map<UserLoginResponseModel>(existingUser);

            foreach (var role in roles)
            {
                response.Role = role;
            }

            response.AccessToken = accessTokenData.Item1;
            response.AccessTokenExpiredTime = accessTokenData.Item2;
            response.RefreshToken = refreshTokenData.Item1;
            response.RefreshTokenExpiryTime = refreshTokenData.Item2;
            return new ApiSuccessResult<UserLoginResponseModel>(response, "Đăng nhập thành công.");
        }
    }
}
