using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Permissions.ViewModels.Permission
{
    public class GroupPermissionVM
    {
        public int GroupId { get; set; }
        public List<PagePermissionVM> PagePermissions { get; set; } = new List<PagePermissionVM>();
    }
}
