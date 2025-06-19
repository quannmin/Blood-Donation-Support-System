
namespace Blood.ModelViews.AuthModelViews.Request
{
    public class ResetPasswordRequestModel
    {
        public string Email { get; set; }
        public string Code { get; set; } // <-- Thay Token bằng Code
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
