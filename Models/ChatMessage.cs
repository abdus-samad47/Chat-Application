using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Time_Chat_Application.Models
{
    public class ChatMessage
    {
        [Key]
        public int MessageId { get; set; }
        public string MessageText { get; set; } = null!;
        
        [Required]
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public DateTime SentAt { get; set; }
        public int ChatRoomID { get; set; }

        [ForeignKey("SenderID")]
        public User Sender { get; set; } = null!;

        [ForeignKey("ReceiverID")]
        public User? Receiver { get; set; }

        [ForeignKey("ChatRoomID")]
        public ChatRoom? ChatRoom  { get; set; }

    }
}
