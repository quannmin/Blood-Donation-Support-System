using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BlogPostModelViews
{
    public class BlogPostCreateModelView
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        public IFormFile? Image { get; set; }
    }
}
