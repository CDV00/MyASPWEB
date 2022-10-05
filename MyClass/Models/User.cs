using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            Img = "~/Content/images/avatars/add.png";
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn phải nhập tài khoản")]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự")]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public int Gender { get; set; }
        [Required]
        public string Roles { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Img { get; set; }
        public int? CountError { get; set; }
        public int Access { get; set; }
        public DateTime? Created_at { get; set; }
        public string Created_by { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Updated_by { get; set; }
        public int Status { get; set; }
    }
}
