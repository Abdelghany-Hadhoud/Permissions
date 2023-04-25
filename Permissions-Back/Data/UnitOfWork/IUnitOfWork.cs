using Permissions.Data.Repositories;
using Permissions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permissions.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
        bool SaveChanges();
        int SaveChangesRes();
        void Dispose(bool disposing);

        IRepository<Group> _GroupRepository { get; }
        IRepository<Permission> _PermissionRepository { get; }
    }
}
