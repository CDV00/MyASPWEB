using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên Menu không được để  trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Link không dược để trống!")]
        public string Link { get; set; }
        [Required]
        public string Type { get; set; }

        public int? Orders { get; set; }
        [Required]
        public string Position { get; set; }
        public int? TableId { get; set; }
        public int ParentId { get; set; }
        public DateTime? Created_at { get; set; }
        public string Created_by { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Updated_by { get; set; }
        public int Status { get; set; }
    }
}
