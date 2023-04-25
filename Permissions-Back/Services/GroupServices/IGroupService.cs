using Permissions.ViewModels.Group;
using Permissions.ViewModels;

namespace Permissions.Services.GroupServices
{
    public interface IGroupService
    {
        ResultViewModel GetGroupsList();
        ResultViewModel AddGroup(AddGroupVM groupVM);
        ResultViewModel UpdateGroup(UpdateGroupVM groupVM);
        ResultViewModel DeleteGroup(int groupId);
    }
}
