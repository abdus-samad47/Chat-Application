namespace Real_Time_Chat_Application.Models.DTOs
{
    public class CreateChatMessageDTO
    {
        public string MessageText { get; set; } = null!;
        public DateTime SentAt { get; set; }
        public int UserId { get; set; }
        public int ChatRoomId { get; set; }
    }
}
