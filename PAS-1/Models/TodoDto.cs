namespace PAS_1.Models
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool? Finished { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
    public class CreateTodoDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool? Finished { get; set; }
    }
    
    public class UpdateTodoDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? Finished { get; set; }
    }
}
