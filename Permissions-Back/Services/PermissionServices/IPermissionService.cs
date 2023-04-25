using Permissions.ViewModels;
using Permissions.ViewModels.Permission;

namespace Permissions.Services.PermissionServices
{
    public interface IPermissionService
    {
        ResultViewModel GetGroupPermissions(int groupId);
        ResultViewModel UpdateGroupPermissions(List<GroupPermissionVM> groupPermissionVMs);
    }
}
