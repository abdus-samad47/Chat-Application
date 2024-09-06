namespace Real_Time_Chat_Application.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = null!;

        public ICollection<ChatMessage> Messages { get; set; } = null!;
        public ICollection<User> Users { get; set; } = null!;
    }
}
