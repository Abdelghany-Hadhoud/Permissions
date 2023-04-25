using Permissions.Data.Repositories;
using Permissions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permissions.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ProjectContext Context = null;
        //private IRepository<User> userRepository;

        public UnitOfWork(ProjectContext context)
        {
            Context = context;
        }
        private IRepository<Group> groupRepository;
        public IRepository<Group> _GroupRepository
        {
            get
            {
                if (groupRepository == null)
                {
                    groupRepository = new GenericRepository<Group>(Context);
                }
                return groupRepository;
            }
        }
        private IRepository<Permission> permissionRepository;
        public IRepository<Permission> _PermissionRepository
        {
            get
            {
                if (permissionRepository == null)
                {
                    permissionRepository = new GenericRepository<Permission>(Context);
                }
                return permissionRepository;
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await Context.SaveChangesAsync();
                return result > 0;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SaveChanges()
        {
            try
            {

                var res = Context.SaveChanges();
                return res > 0;
            }
            catch (Exception ex)
            {

                return false;

            }
        }
        public int SaveChangesRes()
        {
            try
            {

                var res = Context.SaveChanges();
                return res;
            }
            catch (Exception ex)
            {

                return -1;

            }
        }

        private bool disposed = false;


        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
