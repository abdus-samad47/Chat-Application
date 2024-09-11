using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Real_Time_Chat_Application.Models
{
    public class ChatRoomUser
    {
        [Required]
        public int ChatRoomID { get; set; }

        [Required]
        public int UserID { get; set; }

        public DateTime JoinedAt { get; set; }

        public string? Role { get; set; }

        // Navigation properties
        [ForeignKey("ChatRoomID")]
        public ChatRoom ChatRoom { get; set; } = null!;

        [ForeignKey("UserID")]
        public User User { get; set; } = null!;
    }
}
