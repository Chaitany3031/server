using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project>Projects { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<TaskAssignment> Assignments { get; set; }
        public DbSet<ProjectMember> Members { get; set; }
    }
}
