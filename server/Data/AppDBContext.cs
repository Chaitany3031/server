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
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        //fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //user
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(u=>u.UserEmail)
                .IsRequired()
                .HasMaxLength(100);
            });

            //project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.ProjectId);
                entity.Property(p=>p.ProjectName)
                .IsRequired()
                .HasMaxLength (100);
                entity.Property(p => p.ProjectDescription)
                .IsRequired()
                .HasMaxLength(1000);
                //creator relo
                entity.HasOne(p=>p.Creator)
                      .WithMany(u=>u.Projects)
                      .HasForeignKey(p=>p.CreatorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //taskitem
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.TaskItemId);
                entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(t => t.Status)
                .HasMaxLength(50);

                //project relo (project > taskitem)
                entity.HasOne(t=>t.Project)
                .WithMany(p=>p.TaskItems)
                .HasForeignKey(t=>t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

                //creator relo (user > task)
                entity.HasOne(t=>t.Creator)
                .WithMany(u=>u.TaskItems)
                .HasForeignKey(t=>t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            //comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.CommentId);
                entity.Property(c=>c.Content)
                .IsRequired()
                .HasMaxLength(1000);
                //comment > task relo
                entity.HasOne(c=>c.User)
                .WithMany(u=>u.Comments)
                .HasForeignKey(c=>c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

                //user relo
                entity.HasOne(c=>c.User)
                .WithMany(u=>u.Comments)
                .HasForeignKey(c=>c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            //project member
            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(pm => new {pm.UserId,pm.ProjectId });

                entity.Property(pm => pm.Role)
                .IsRequired()
                .HasMaxLength(50);

                entity.HasOne(pm=>pm.User)
                .WithMany(u=>u.ProjectMembers)
                .HasForeignKey(pm=>pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pm => pm.Project)
                .WithMany(pm => pm.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            });


            //TaskAssignment
            modelBuilder.Entity<TaskAssignment>(entity =>
            {
                entity.HasKey(ta => new { ta.UserId, ta.TaskItemId });
                entity.HasOne(ta=>ta.User)
                .WithMany(u=>u.TaskAssignments)
                .HasForeignKey(ta=>ta.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ta=>ta.TaskItem)
                .WithMany(t=>t.TaskAssignments)
                .HasForeignKey(ta=>ta.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            //role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId);

                entity.Property(r=>r.RoleName)
                .IsRequired()
                .HasMaxLength(50);
            });

            //userrole
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur=>ur.User)
                .WithMany(u=>u.UserRoles)
                .HasForeignKey(ur=>ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur=>ur.Role)
                .WithMany(r=>r.UserRoles)
                .HasForeignKey(ur=>ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
