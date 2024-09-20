namespace Real_Time_Chat_Application.Models.DTOs
{
    public class ChatMessageDTO
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
    }
}
