using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Real_Time_Chat_Application.Entities;

namespace Real_Time_Chat_Application.Models
{
    public class ChatRoomUser
    {
        public int Id { get; set; }

        public int ChatRoomId { get; set; }

        public int UserId { get; set; }

        public DateTime JoinedAt { get; set; }

        public string? Role { get; set; }

        // Navigation properties
        [ForeignKey("ChatRoomId")]
        public ChatRoom ChatRoom { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
