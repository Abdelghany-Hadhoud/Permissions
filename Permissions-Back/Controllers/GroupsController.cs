using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Permissions.Services.GroupServices;
using Permissions.ViewModels.Group;

namespace Permissions.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            this._groupService = groupService;
        }
        [HttpGet]
        public IActionResult GetGroups()
        {
            var res = _groupService.GetGroupsList();
            return Ok(res);
        }
        [HttpPost]
        public IActionResult AddGroup([FromBody] AddGroupVM addGroupVM)
        {
            if (ModelState.IsValid)
            {
                var res = _groupService.AddGroup(addGroupVM);
                return Ok(res);
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPost]
        public IActionResult UpdateGroup([FromBody] UpdateGroupVM updateGroupVM)
        {
            if (ModelState.IsValid)
            {
                var res = _groupService.UpdateGroup(updateGroupVM);
                return Ok(res);
            }
            else
                return BadRequest(ModelState);
        }
        [HttpGet]
        public IActionResult DeleteGroup(int groupId)
        {
            var res = _groupService.DeleteGroup(groupId);
            return Ok(res);
        }
    }

}
