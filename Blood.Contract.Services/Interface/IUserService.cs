using Blood.ModelViews.UserModelViews;

namespace Blood.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<IList<UserResponseModel>> GetAll();
    }
}
