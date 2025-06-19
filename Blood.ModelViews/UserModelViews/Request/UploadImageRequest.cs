using Microsoft.AspNetCore.Http;

namespace Blood.ModelViews.UserModelViews.Request
{
    public class UploadImageRequest
    {
        public IFormFile Image { get; set; }
    }
}
