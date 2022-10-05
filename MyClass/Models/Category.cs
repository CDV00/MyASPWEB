using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Categorys")]
    public class Category
    {
        [Key]
        public int Id { get; set;}

        [Required(ErrorMessage = "Tên loại sản phẩm không để rỗng")]
        public string Name { get; set;}
        public string Slug { get; set;}
        public int? ParentId { get; set;}
        public int? Orders { get; set;}
        public string Image { get; set; }
        public string Title { get; set; }

        [Required(ErrorMessage = "Từ khóa SEO không để rỗng")]
        public string MetaKey { get; set; }

        [Required(ErrorMessage = "Mô tả SEO không để rỗng")]
        public string MetaDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}
