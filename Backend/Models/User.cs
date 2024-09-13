using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public ICollection<ChatMessage> SentMessages { get; set; }
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
        public ICollection<ChatRoom> CreatedChatRooms { get; set; }
        public ICollection<ChatRoomUser> ChatRoomUsers { get; set; }
    }
}
