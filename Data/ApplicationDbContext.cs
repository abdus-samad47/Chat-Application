using Microsoft.EntityFrameworkCore;
using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public DbSet<ChatRoom> ChatRooms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatRoom>()
                .HasMany(u => u.Users)
                .WithMany(u => u.ChatRooms)
                .UsingEntity(j => j.ToTable("UserChatRooms"));

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessages");
        }
        
    }
}
