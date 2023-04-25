using Permissions.Data.UnitOfWork;
using Permissions.ViewModels;
using Permissions.ViewModels.Page;

namespace Permissions.Services.PageServices
{
    public class PageService : IPageService
    {
        private ResultViewModel returnResult;
        public PageService()
        {
            returnResult = new ResultViewModel();
            returnResult.BindResultViewModel(false, "an error occurred, try again later", "حدث خطا، حاول مرة اخري", StatusCodes.Status400BadRequest, null);
        }
        public ResultViewModel GetPagesList()
        {

            var pages = new List<PageVM>()
            {
                new PageVM() { Id =1 ,EnglishName = "Home",ArabicName = "الرئيسية"},
                new PageVM() { Id =2 ,EnglishName = "Contacts",ArabicName = "الإتصال"},
                new PageVM() { Id =3 ,EnglishName = "Setting",ArabicName = "الإعدادات"}
            };
            returnResult.BindResultViewModel(true, "Data Retrieved Successfully", "تم بنجاح", StatusCodes.Status200OK, pages);

            return returnResult;
        }
    }
}
