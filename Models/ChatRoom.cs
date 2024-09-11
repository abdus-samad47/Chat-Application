using System.ComponentModel.DataAnnotations;

namespace Real_Time_Chat_Application.Models
{
    public class ChatRoom
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        public User Creator { get; set; } = null!;
        public ICollection<ChatMessage>? Messages { get; set; }
        public ICollection<ChatRoomUser>? ChatRoomUsers { get; set; }
    }
}
