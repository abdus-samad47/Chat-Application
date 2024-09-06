namespace Real_Time_Chat_Application.Models.DTOs
{
    public class CreateUserDTO
    {
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
