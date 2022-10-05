using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên sản phẩm không để rỗng")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Id loại sản phẩm không để rỗng")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Id nhà sản xuất không để rỗng")]
        public int SupplierId { get; set; }
        public string Slug { get; set;}
        public string Image { get; set;}

        [Required]
        public string Detail { get; set; }

        public int Number { get; set; }

        public float Price { get; set; }

        public float PriceSale { get; set; }
        [Required]
        public string Metakey { get; set; }
        [Required]
        public string Metadesc { get; set; }
        public DateTime? Created_at { get; set; }
        public string Created_by { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Updated_by { get; set; }
        public int Status { get; set; }
    }
}
