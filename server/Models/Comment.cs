namespace server.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        //Task Relo
        public int TaskId { get; set; }
        public TaskItem TaskItem { get; set; } = new TaskItem();\

        //User Ownership
        public int UserId { get; set; }
        public User User {  get; set; } = new User();
    }
}
