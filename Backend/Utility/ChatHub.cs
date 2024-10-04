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

        //private static readonly Dictionary<string, string> _connections = new();
        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        //public override Task OnConnectedAsync()
        //{
        //    var userId = Context.UserIdentifier;

        //    if (userId != null)
        //    {
        //        _connections[userId] = Context.ConnectionId;
        //    }

        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    var userId = Context.UserIdentifier;

        //    if (userId != null)
        //    {
        //        _connections.Remove(userId);
        //    }

        //    return base.OnDisconnectedAsync(exception);
        //}

        public async Task SendMessage(CreateChatMessageDTO createChatMessageDTO)
        {
            //var senderId = createChatMessageDTO.SenderId.ToString();

            //var receiverId = createChatMessageDTO.ReceiverId;

            //var userId = Context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            //if (!_connections.ContainsKey(senderId))
            //{
            //    Console.Error.WriteLine($"User {senderId} is not connected.");
            //    return;
            //}

            try
            {
                var message = new ChatMessage
                {
                    MessageText = createChatMessageDTO.MessageText,
                    SenderId = createChatMessageDTO.SenderId,
                    ReceiverId = int.Parse(createChatMessageDTO.ReceiverId),
                    SentAt = createChatMessageDTO.SentAt
                };

                await _context.ChatMessages.AddAsync(message);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Sending message: {message.MessageText} from {message.SenderId} to {message.ReceiverId}");

                //await Clients.User(receiverId).SendAsync("ReceiveMessage", createChatMessageDTO.MessageText);

                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }
    }
}
