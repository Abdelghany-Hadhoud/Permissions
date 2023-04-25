using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Permissions.Services.GroupServices;
using Permissions.Services.PageServices;

namespace Permissions.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _pageService;
        public PagesController(IPageService pageService)
        {
            this._pageService = pageService;
        }
        [HttpGet]
        public IActionResult GetPages()
        {
            var res = _pageService.GetPagesList();
            return Ok(res);
        }
    }
}
