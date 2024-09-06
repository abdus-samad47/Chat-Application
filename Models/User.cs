namespace Real_Time_Chat_Application.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;

        public ICollection<ChatMessage> Messages { get; set; } = null!;
        public ICollection<ChatRoom> ChatRooms { get; set; } = null!; 
    }

}
