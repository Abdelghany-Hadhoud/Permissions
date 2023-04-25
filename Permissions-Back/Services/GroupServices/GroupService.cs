using Permissions.Data.UnitOfWork;
using Permissions.Entities;
using Permissions.ViewModels.Group;
using Permissions.ViewModels;

namespace Permissions.Services.GroupServices
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResultViewModel returnResult;
        public GroupService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            returnResult = new ResultViewModel();
            returnResult.BindResultViewModel(false, "an error occurred, try again later", "حدث خطا، حاول مرة اخري", StatusCodes.Status400BadRequest, null);
        }
        public ResultViewModel GetGroupsList()
        {
            try
            {
                var groups = _unitOfWork._GroupRepository.GetAll(e => e.IsActive == true);
                returnResult.BindResultViewModel(true, "Data Retrieved Successfully", "تم بنجاح", StatusCodes.Status200OK, groups);
            }
            catch (Exception ex)
            {
                returnResult.BindResultViewModel(false, ex.Message, ex.Message, StatusCodes.Status400BadRequest, null);
            }
            return returnResult;
        }
        public ResultViewModel AddGroup(AddGroupVM groupVM)
        {
            try
            {
                if (groupVM != null)
                {
                    var group = new Group()
                    {
                        EnglishName = groupVM.EnglishName,
                        ArabicName = groupVM.ArabicName,
                    };
                    _unitOfWork._GroupRepository.Add(group);
                    var res = _unitOfWork.SaveChanges();
                    if (res)
                        returnResult.BindResultViewModel(true, "Group Created Successfully", "تم بنجاح", StatusCodes.Status200OK, group);
                }
            }
            catch (Exception ex)
            {
                returnResult.BindResultViewModel(false, ex.Message, ex.Message, StatusCodes.Status400BadRequest, null);
            }
            return returnResult;
        }
        public ResultViewModel UpdateGroup(UpdateGroupVM groupVM)
        {
            try
            {
                var group = GetGroupById(groupVM.Id);
                if (group != null)
                {
                    group.EnglishName = groupVM.EnglishName;
                    group.ArabicName = groupVM.ArabicName;
                    var res = _unitOfWork.SaveChanges();
                    if (res)
                        returnResult.BindResultViewModel(true, "Group Updated Successfully", "تم بنجاح", StatusCodes.Status200OK, null);
                }
            }
            catch (Exception ex)
            {
                returnResult.BindResultViewModel(false, ex.Message, ex.Message, StatusCodes.Status400BadRequest, null);
            }
            return returnResult;
        }
        public ResultViewModel DeleteGroup(int groupId)
        {
            try
            {
                var group = GetGroupById(groupId);
                if (group != null)
                {
                    group.IsActive = false;
                    var res = _unitOfWork.SaveChanges();
                    if (res)
                        returnResult.BindResultViewModel(true, "Group Deleted Successfully", "تم بنجاح", StatusCodes.Status200OK, null);
                }
            }
            catch (Exception ex)
            {
                returnResult.BindResultViewModel(false, ex.Message, ex.Message, StatusCodes.Status400BadRequest, null);
            }
            return returnResult;
        }
        private Group GetGroupById(int groupId)
        {
            var group = _unitOfWork._GroupRepository.GetAllAsQueryable().Where(b => b.IsActive == true && b.Id == groupId).FirstOrDefault();
            return group;
        }
    }

}
