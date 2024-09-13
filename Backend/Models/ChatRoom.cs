using Real_Time_Chat_Application.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Time_Chat_Application.Models
{
    public class ChatRoom
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        [ForeignKey("UserId")]
        public User Creator { get; set; }
        public ICollection<ChatMessage>? Messages { get; set; }
        public ICollection<ChatRoomUser>? ChatRoomUsers { get; set; }
    }
}
