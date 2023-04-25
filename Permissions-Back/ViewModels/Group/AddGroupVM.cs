using System.ComponentModel.DataAnnotations;

namespace Permissions.ViewModels.Group
{
    public class AddGroupVM
    {
        [Required]
        public string EnglishName { get; set; }
        [Required]
        public string ArabicName { get; set; }
    }
}
