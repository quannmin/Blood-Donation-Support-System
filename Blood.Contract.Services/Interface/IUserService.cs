using Blood.Core.APIResponse;
using Blood.Core;
using Blood.ModelViews.AuthModelViews.Request;
using Blood.ModelViews.AuthModelViews.Response;
using Blood.ModelViews.UserModelViews;
using Blood.ModelViews.UserModelViews.Request;
using Blood.ModelViews.UserModelViews.Response;

namespace Blood.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResult<EmployeeLoginResponseModel>> RefreshToken(NewRefreshTokenRequestModel request);
        Task<ApiResult<DashboardUserCreateResponse>> GetUsersByCreateTime();


        #region Authen User
        Task<ApiResult<UserLoginResponseModel>> UserLogin(UserLoginRequestModel request);

        Task<ApiResult<object>> RegisterWithRole(UserRegisterRequestModel request, string roleId);
        Task<ApiResult<object>> RegisterUser(UserRegisterRequestModel request);
        Task<ApiResult<object>> RegisterDoctor(UserRegisterRequestModel request);
        Task<ApiResult<UserLoginResponseModel>> ConfirmUserRegister(ConfirmUserRegisterRequest request);

        Task<ApiResult<object>> ForgotPassword(ForgotPasswordRequest request);
        Task<ApiResult<object>> ResetPassword(ResetPasswordRequestModel request);
        #endregion

        #region Authen Admin/Owner/Coach
        Task<ApiResult<EmployeeLoginResponseModel>> EmployeeLogin(EmployeeLoginRequestModel request);
        Task<ApiResult<object>> EmployeeForgotPassword(ForgotPasswordRequest request);
        Task<ApiResult<object>> EmployeeResetPassword(ResetPasswordRequestModel request);
        #endregion

        #region User

        Task<ApiResult<object>> UpdateUserProfile(UpdateUserProfileRequest request);
        // Get user pagination 
        Task<ApiResult<BasePaginatedList<UserResponseModel>>> GetUserPagination(string? Email, int? PageIndex, int? PageSize);
        Task<ApiResult<UserResponseModel>> GetUserById(int Id);
        Task<ApiResult<object>> DeleteUser(DeleteUserRequest request);
        Task<ApiResult<object>> UpdateUserStatus(UpdateUserStatusRequest request);



        #endregion

        #region Admin/Owner/Coach

        Task<ApiResult<object>> CreateEmployee(CreateEmployeeRequest request);
        Task<ApiResult<object>> UpdateDoctorProfile(UpdateEmployeeProfileRequest request);
        Task<ApiResult<object>> UpdateEmployeeStatus(UpdateUserStatusRequest request);
        Task<ApiResult<object>> DeleteEmployee(DeleteUserRequest request);
        Task<ApiResult<BasePaginatedList<EmployeeResponseModel>>> GetDoctorPagination(string? Email, int? PageIndex, int? PageSize);
        Task<ApiResult<List<EmployeeResponseModel>>> GetAllDoctor();
        Task<ApiResult<List<EmployeeResponseModel>>> GetAllEmployee();
        Task<ApiResult<List<UserResponseModel>>> GetAllUser();
        Task<ApiResult<EmployeeResponseModel>> GetEmployeeById(int Id);
        Task<ApiResult<UploadImageResponseModel>> UploadImage(UploadImageRequest request);

        Task<ApiResult<object>> ResendOtpAsync(string email);



        #endregion
    }
}
