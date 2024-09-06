namespace Real_Time_Chat_Application.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string MessageText { get; set; } = null!;
        public DateTime SentAt { get; set; }

        //Foreign Keys
        public int UserId { get; set; }
        public int ChatRoomId  { get; set; }

        //Navigation Properties
        public ChatRoom ChatRoom { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
