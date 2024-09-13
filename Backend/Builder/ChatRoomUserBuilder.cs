using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Builder
{
    public class ChatRoomUserBuilder
    {
        internal static void BuildChatRoomUser(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<ChatRoomUser>();
            builder.ToTable("ChatRoomUser");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("ChatRoomUser_Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.ChatRoomId).HasColumnName("ChatRoomUser_RoomId").IsRequired();
            builder.Property(c => c.UserId).HasColumnName("ChatRoomUser_UserId").IsRequired();
            builder.Property(c => c.JoinedAt).HasColumnName("ChatRoomUser_JoinedAt").IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.Role).HasColumnName("ChatRoomUser_Role").IsRequired().HasMaxLength(100).IsUnicode(true);

            // Configuring relationships
            builder.HasOne(cru => cru.ChatRoom)
                .WithMany(cr => cr.ChatRoomUsers)
                .HasForeignKey(cru => cru.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cru => cru.User)
                .WithMany(u => u.ChatRoomUsers)
                .HasForeignKey(cru => cru.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
