namespace server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        //user can create multiple projects
        public ICollection<Project> ? Projects { get; set; }
        //task assignment relo
        public ICollection<TaskAssignment> ? TaskAssignments { get; set; }
        //projectmember relation
        public ICollection<ProjectMember> ? ProjectMembers { get; set; } 
        //user can create multiple tasks
        public ICollection<TaskItem>? TaskItems { get; set; } 
        //user can comment
        public ICollection<Comment>? Comments { get; set; } 
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
