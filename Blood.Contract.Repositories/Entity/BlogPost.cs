using Blood.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Repositories.Entity
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; } // Tiêu đề

        public string Content { get; set; } // Nội dung

        public string Author { get; set; } // Tác giả

        public string ImageUrl { get; set; } // URL hình ảnh (không dùng JSON)
    }
}
