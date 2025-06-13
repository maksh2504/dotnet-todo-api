using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS_1.Entities
{
    [Table("todo")]
    public class Todo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { set; get; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("finished")]
        public bool Finished { get; set; } = false;
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("user_id")]
        public int? UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}