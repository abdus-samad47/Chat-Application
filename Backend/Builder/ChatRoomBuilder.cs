using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Builder
{
    public class ChatRoomBuilder
    {
        internal static void BuildChatRoom(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<ChatRoom>();
            builder.ToTable("ChatRooms");
            builder.HasKey(r => r.RoomId);
            builder.Property(r => r.RoomId).HasColumnName("Room_Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(r => r.RoomName).HasColumnName("Room_Name").IsRequired().HasMaxLength(255).IsUnicode(true);
            builder.Property(r => r.CreatedAt).HasColumnName("Room_CreatedAt").IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(r => r.CreatedBy).HasColumnName("Room_CreatedBy").IsRequired();

            // Configuring relationships
            builder.HasMany(cr => cr.Messages)
                .WithOne(m => m.ChatRoom)
                .HasForeignKey(m => m.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cr => cr.ChatRoomUsers)
                .WithOne(cru => cru.ChatRoom)
                .HasForeignKey(cru => cru.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
