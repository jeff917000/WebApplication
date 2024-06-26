using System.ComponentModel.DataAnnotations;

namespace LoginDemo.Models
{
    public class UserTableItemAttributes
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "帳號")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string UserPassword { get; set; }
        [Required]
        [Display(Name = "權限")]
        public int UserRole { get; set; }
    }
}
