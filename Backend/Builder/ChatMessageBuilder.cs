using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Real_Time_Chat_Application.Entities;
using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Builder
{
    public class ChatMessageBuilder
    {
        internal static void BuildChatMessage(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<ChatMessage>();
            builder.ToTable("ChatMessages");
            builder.HasKey(m => m.MessageId);
            builder.Property(m => m.MessageId).HasColumnName("ChatMessage_Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(m => m.MessageText).HasColumnName("ChatMessage_Text").IsRequired().HasMaxLength(500).IsUnicode(true);
            builder.Property(m => m.SenderId).HasColumnName("ChatMessage_Sender").IsRequired();
            builder.Property(m => m.ReceiverId).HasColumnName("ChatMessage_Receiver").IsRequired(false);
            builder.Property(m => m.SentAt).HasColumnName("ChatMessage_SentAt").IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(m => m.ChatRoomId).HasColumnName("ChatMessage_RoomId").IsRequired(false);

            // Configuring relationships
            builder.HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.RoomId)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.ChatRoom)
                .WithMany(cr => cr.Messages)
                .HasForeignKey(m => m.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
