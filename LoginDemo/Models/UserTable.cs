using System.ComponentModel.DataAnnotations;

namespace LoginDemo.Models
{
    [MetadataType(typeof(UserTableItemAttributes))]
    public class UserTable
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int UserRole { get; set; }

        public UserTable()
        {
            UserId = 0;
            UserName = string.Empty;
            UserPassword = string.Empty;
            UserRole = 0;
        }
    }
}
