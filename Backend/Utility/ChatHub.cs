using Microsoft.AspNetCore.SignalR;
using Real_Time_Chat_Application.Models.DTOs;
using Real_Time_Chat_Application.Models;
using Real_Time_Chat_Application.Data;
using System.Security.Claims;

namespace Real_Time_Chat_Application.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ApplicationDbContext context, ILogger<ChatHub> logger)
        {
            _context = context;
            _logger = logger;
            _logger = logger;
        }
        public async Task SendMessage(CreateChatMessageDTO createChatMessageDTO)
        {
            var sender = await _context.Users.FindAsync(createChatMessageDTO.SenderId);
            var receiver = await _context.Users.FindAsync(createChatMessageDTO.ReceiverId);

            if (sender == null || receiver == null)
            {
                throw new Exception("Sender or Receiver not found.");
            }

            try
            {
                var message = new ChatMessage
                {
                    MessageText = createChatMessageDTO.MessageText,
                    SenderId = createChatMessageDTO.SenderId,
                    ReceiverId = createChatMessageDTO.ReceiverId,
                    ChatRoomId = createChatMessageDTO.ChatRoomId,
                    SentAt = createChatMessageDTO.SentAt
                };

                //_logger.LogInformation($"User {message.SenderId} is sending a message to {message.ReceiverId} in group {message.ChatRoomId}.");

                await _context.ChatMessages.AddAsync(message);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Sending message: {message.MessageText} from {message.SenderId} to {message.ReceiverId}");

                await Clients.All.SendAsync("ReceiveMessage", new
                {
                    message.MessageText,
                    message.SenderId,
                    message.ReceiverId,
                    message.ChatRoomId,
                    message.SentAt,
                    SenderUsername = sender.Username,
                    ReceiverUsername = receiver.Username
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }
    }
}
