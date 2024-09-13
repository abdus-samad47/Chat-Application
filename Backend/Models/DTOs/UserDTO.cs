namespace Real_Time_Chat_Application.Models.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
    }
}
