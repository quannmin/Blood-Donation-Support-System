using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BlogPostModelViews
{
    public class BlogPostUpdateModelView
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public IFormFile? Image { get; set; }
    }

}
