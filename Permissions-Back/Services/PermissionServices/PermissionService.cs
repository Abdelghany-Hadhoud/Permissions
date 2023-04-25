using Microsoft.EntityFrameworkCore;
using Permissions.Data.UnitOfWork;
using Permissions.ViewModels;
using Permissions.ViewModels.Permission;

namespace Permissions.Services.PermissionServices
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResultViewModel returnResult;
        public PermissionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            returnResult = new ResultViewModel();
            returnResult.BindResultViewModel(false, "an error occurred, try again later", "حدث خطا، حاول مرة اخري", StatusCodes.Status400BadRequest, null);
        }
        public ResultViewModel GetGroupPermissions(int groupId)
        {
            try
            {
                var permissions = _unitOfWork._PermissionRepository.GetAllQueryableAsNoTracking()
                    .Where(ww => ww.IsActive == true && ww.GroupId == groupId)
                    .Select(w => new PagePermissionVM()
                    {
                        Id = w.Id,
                        PageId = w.PageId,
                        CanAdd = w.CanAdd,
                        CanDelete = w.CanDelete,
                        CanEdit = w.CanEdit,
                        CanView = w.CanView,
                    }).ToList();
                returnResult.BindResultViewModel(true, "Data Retrieved Successfully", "تم بنجاح", StatusCodes.Status200OK, permissions);
            }
            catch (Exception ex)
            {
                returnResult.BindResultViewModel(false, ex.Message, ex.Message, StatusCodes.Status400BadRequest, null);
            }
            return returnResult;
        }
        public ResultViewModel UpdateGroupPermissions(List<GroupPermissionVM> groupPermissionVMs)
        {
            try
            {
                if (groupPermissionVMs != null && groupPermissionVMs.Count > 0)
                {
                    var groupIds = groupPermissionVMs.Select(e => e.GroupId).ToArray();
                    var permissions = _unitOfWork._PermissionRepository.GetAllAsQueryable()
                        .Where(ww => ww.IsActive == true && groupIds.Contains(ww.GroupId))
                        .ToList();
                    foreach (var groupPermission in groupPermissionVMs)
                    {
                        foreach (var permission in groupPermission.PagePermissions)
                        {
                            if (permission.Id > 0)
                            {
                                var entity = permissions.Where(e => e.Id == permission.Id).FirstOrDefault();
                                if (entity != null)
                                {
                                    entity.CanView = permission.CanView;
                                    entity.CanDelete = permission.CanDelete;
                                    entity.CanEdit = permission.CanEdit;
                                    entity.CanAdd = permission.CanAdd;
                                }
                            }
                            else
                            {
                                _unitOfWork._PermissionRepository.Add(new Entities.Permission()
                                {
                                    GroupId = groupPermission.GroupId,
                                    PageId = permission.PageId,
                                    CanAdd = permission.CanAdd,
                                    CanDelete = permission.CanDelete,
                                    CanEdit = permission.CanEdit,
                                    CanView = permission.CanView
                                });
                            }
                        }
                    }
                    var res = _unitOfWork.SaveChangesRes();
                    if (res > 0)
                        returnResult.BindResultViewModel(true, "Data Saved Successfully", "تم بنجاح", StatusCodes.Status200OK, null);
                    if (res == 0)
                        returnResult.BindResultViewModel(true, "No Changes To Save !!", "لا يوجد تغيرات للحفظ !!", StatusCodes.Status200OK, null);
                }
            }
            catch (Exception ex)
            {
                returnResult.BindResultViewModel(false, ex.Message, ex.Message, StatusCodes.Status400BadRequest, null);
            }
            return returnResult;
        }

    }
}
