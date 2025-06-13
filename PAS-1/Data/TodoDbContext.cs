using Microsoft.EntityFrameworkCore;
using PAS_1.Entities;

namespace PAS_1.Data
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
    }
}

// public class TodoDbContext : DbContext
// {
//     public TodoDbContext(DbContextOptions<TodoDbContext> options)
//         : base(options) { }
//
//     public DbSet<User> Users { get; set; }
//     public DbSet<Todo> Todos { get; set; }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);
//
//         modelBuilder.Entity<User>().ToTable("user");
//         modelBuilder.Entity<Todo>().ToTable("todo");
//
//         modelBuilder.Entity<User>(entity =>
//         {
//             entity.HasKey(u => u.Id);
//             // entity.Property(u => u.Username).HasColumnName("username");
//             // entity.Property(u => u.Password).HasColumnName("password");
//         });
//
//         modelBuilder.Entity<Todo>()
//             .HasOne(s => s.User)
//             .WithMany(u => u.Todos)
//             .HasForeignKey(s => s.UserId);
//     }
// }
