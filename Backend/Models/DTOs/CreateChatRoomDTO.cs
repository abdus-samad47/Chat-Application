namespace Real_Time_Chat_Application.Models.DTOs
{
    public class CreateChatRoomDTO
    {
        public string RoomName { get; set; }
        public int CreatedBy { get; set; }
        public List<int> Users { get; set; }
    }
}
