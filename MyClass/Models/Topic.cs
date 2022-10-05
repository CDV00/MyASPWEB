using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Phai nhap")]
        public string Name { get; set; }

        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Orders { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime? Created_at { get; set; }
        public string Created_by { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Updated_by { get; set; }
        public int Status { get; set; }
    }
}
