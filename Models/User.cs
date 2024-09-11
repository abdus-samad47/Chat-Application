using System.ComponentModel.DataAnnotations;

namespace Real_Time_Chat_Application.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? Status { get; set; }

        public ICollection<ChatMessage>? ReceivedMessages { get; set; }
        public ICollection<ChatMessage>? SendMessages { get; set; }
        public ICollection<ChatRoom>? ChatRoomUser { get; set; }
    }

}
