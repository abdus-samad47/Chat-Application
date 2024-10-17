using Microsoft.EntityFrameworkCore;
using Real_Time_Chat_Application.Builder;
using Real_Time_Chat_Application.Entities;
using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }
        //public DbSet<ActivityStream> ActivityStream { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var pipeline = new List<Action<ModelBuilder>>
            {
                UserBuilder.BuildUser,
                ChatMessageBuilder.BuildChatMessage,
                ChatRoomBuilder.BuildChatRoom,
                ChatRoomUserBuilder.BuildChatRoomUser
            };
            pipeline.ForEach(build => build(modelBuilder));
            //// Configuring User entity relationships
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.ReceivedMessages)
            //    .WithOne(m => m.Receiver)
            //    .HasForeignKey(m => m.ReceiverID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.SendMessages)
            //    .WithOne(m => m.Sender)
            //    .HasForeignKey(m => m.SenderID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// Configuring ChatMessage entity relationships
            //modelBuilder.Entity<ChatMessage>()
            //    .HasOne(m => m.Sender)
            //    .WithMany(u => u.SendMessages)
            //    .HasForeignKey(m => m.SenderID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ChatMessage>()
            //    .HasOne(m => m.Receiver)
            //    .WithMany(u => u.ReceivedMessages)
            //    .HasForeignKey(m => m.ReceiverID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ChatMessage>()
            //    .HasOne(m => m.ChatRoom)
            //    .WithMany(cr => cr.Messages)
            //    .HasForeignKey(m => m.ChatRoomID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Configuring ChatRoom entity relationships
            //modelBuilder.Entity<ChatRoom>()
            //    .HasMany(cr => cr.Messages)
            //    .WithOne(m => m.ChatRoom)
            //    .HasForeignKey(m => m.ChatRoomID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ChatRoom>()
            //    .HasMany(cr => cr.ChatRoomUsers)
            //    .WithOne(cru => cru.ChatRoom)
            //    .HasForeignKey(cru => cru.ChatRoomID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Configuring ChatRoomUser entity relationships
            //modelBuilder.Entity<ChatRoomUser>()
            //.HasKey(cru => cru.Id);

            //modelBuilder.Entity<ChatRoomUser>()
            //    .HasOne(cru => cru.ChatRoom)
            //    .WithMany(cr => cr.ChatRoomUsers)
            //    .HasForeignKey(cru => cru.ChatRoomID);

            //modelBuilder.Entity<ChatRoomUser>()
            //    .HasOne(cru => cru.User)
            //    .WithMany(u => u.ChatRoomUsers)
            //    .HasForeignKey(cru => cru.UserID);
        }
        
    }
}
