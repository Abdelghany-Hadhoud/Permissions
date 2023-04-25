using System.ComponentModel.DataAnnotations;

namespace Permissions.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ArabicName { get; set; }
        [Required]
        public string EnglishName { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Permission> PermissionList { get; set; }
    }
}
