using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS_1.Entities
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("username")]
        public string Username { set; get; }
        
        [Required]
        [Column("password")]
        public string Password { get; set; }
        
        [Column("refresh_token")]
        public string? RefreshToken { get; set; }
        
        [Column("refresh_token_exp")]
        public DateTime? RefreshTokenExpTime { get; set; }

        public ICollection<Todo>? Todos { get; set; }
    }
}