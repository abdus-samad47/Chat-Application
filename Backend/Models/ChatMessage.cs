using Real_Time_Chat_Application.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Time_Chat_Application.Models
{
    public class ChatMessage
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime SentAt { get; set; }
        public int ChatRoomId { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }

        [ForeignKey("ChatRoomId")]
        public ChatRoom ChatRoom { get; set; }

    }
}