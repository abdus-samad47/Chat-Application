namespace Real_Time_Chat_Application.Models.DTOs
{
    public class ChatRoomDTO
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }
}
