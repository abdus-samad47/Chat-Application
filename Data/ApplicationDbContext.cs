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
        public DbSet<ChatRoomUser> ChatRoomUsers { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring User entity relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SendMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuring ChatMessage entity relationships
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SendMessages)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.ChatRoom)
                .WithMany(cr => cr.Messages)
                .HasForeignKey(m => m.ChatRoomID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring ChatRoom entity relationships
            modelBuilder.Entity<ChatRoom>()
                .HasMany(cr => cr.Messages)
                .WithOne(m => m.ChatRoom)
                .HasForeignKey(m => m.ChatRoomID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatRoom>()
                .HasMany(cr => cr.ChatRoomUsers)
                .WithOne(cru => cru.ChatRoom)
                .HasForeignKey(cru => cru.ChatRoomID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring ChatRoomUser entity relationships
            modelBuilder.Entity<ChatRoomUser>()
            .HasKey(cru => new { cru.ChatRoomID, cru.UserID });

            modelBuilder.Entity<ChatRoomUser>()
                .HasOne(cru => cru.ChatRoom)
                .WithMany(cr => cr.ChatRoomUsers)
                .HasForeignKey(cru => cru.ChatRoomID);

            modelBuilder.Entity<ChatRoomUser>()
                .HasOne(cru => cru.User)
                .WithMany(u => u.ChatRoomUsers)
                .HasForeignKey(cru => cru.UserID);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
