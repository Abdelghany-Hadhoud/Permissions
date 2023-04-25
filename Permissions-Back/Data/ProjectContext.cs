using Permissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace Permissions.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
