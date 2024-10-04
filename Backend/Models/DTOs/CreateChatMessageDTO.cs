namespace Real_Time_Chat_Application.Models.DTOs
{
    public class CreateChatMessageDTO
    {
        public string MessageText { get; set; }
        public int SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public int? ChatRoomId { get; set; }
        public DateTime SentAt { get; set; }
    }
}
