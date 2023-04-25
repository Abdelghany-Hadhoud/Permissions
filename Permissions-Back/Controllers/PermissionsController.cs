using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Permissions.Services.PermissionServices;
using Permissions.ViewModels.Permission;

namespace Permissions.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            this._permissionService = permissionService;
        }
        [HttpGet]
        public IActionResult GetGroupPermissions(int groupId)
        {
            var res = _permissionService.GetGroupPermissions(groupId);
            return Ok(res);
        }
        [HttpPost]
        public IActionResult UpdateGroupPermissions(List<GroupPermissionVM> groupPermissionVMs)
        {
            var res = _permissionService.UpdateGroupPermissions(groupPermissionVMs);
            return Ok(res);
        }
    }
}
