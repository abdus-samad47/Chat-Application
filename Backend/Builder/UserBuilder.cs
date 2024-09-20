using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Real_Time_Chat_Application.Entities;
using System.Reflection.Emit;

namespace Real_Time_Chat_Application.Builder
{
    public class UserBuilder
    {
        internal static void BuildUser(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<User>();
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId).HasColumnName("User_Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(u => u.Username).HasColumnName("User_name").IsRequired().HasMaxLength(50).IsUnicode(true);
            builder.Property(u => u.Email).HasColumnName("User_Email").IsRequired().HasMaxLength(100).IsUnicode(true);
            builder.Property(u => u.PhoneNumber).HasColumnName("User_PhoneNumber").HasMaxLength(20).IsUnicode(true);
            builder.Property(u => u.PasswordHash).HasColumnName("User_Password").IsRequired().HasMaxLength(255).IsUnicode(false);
            builder.Property(u => u.Salt).HasColumnName("User.Salt").IsRequired().HasMaxLength(255).IsUnicode(false);
            builder.Property(u => u.CreatedAt).HasColumnName("User_CreatedAt").IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(u => u.Status).HasColumnName("User_Status").IsRequired().HasMaxLength(20).HasDefaultValue("Offline").IsUnicode(true);

            // Add unique constraints
            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            // Configuring relationships
            builder.HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.CreatedChatRooms)
                .WithOne(cr => cr.Creator)
                .HasForeignKey(cr => cr.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ChatRoomUsers)
                .WithOne(cru => cru.User)
                .HasForeignKey(cru => cru.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
