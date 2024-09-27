using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

public class ChatHub : Hub
{
        private readonly ILogger<ChatHub> _logger;
        
    public ChatHub (ILogger<ChatHub> logger)
    {
        _logger = logger;
    }
    public override async Task OnConnectedAsync()
    {

        // Log the user's connection
        var userId = Context.UserIdentifier;

        _logger.LogInformation($"User connected with UserIdentifier: {userId}");

        if (userId != null)
        {
            // Optional: Notify the user that they are connected
            await Clients.Caller.SendAsync("Connected", $"You are connected as user {userId}");
        }
        await base.OnConnectedAsync();
    }

    public async Task SendMessage( int receiverId, string messageText)
    {
        // Ensure the user identifier is set
        var senderId = Context.UserIdentifier;
        if (senderId == null)
        {
            throw new HubException("User identifier is null.");
        }

        // You may want to save the message to the database here
        // var message = new ChatMessage { SenderId = int.Parse(senderId), ReceiverId = receiverId, MessageText = messageText, SentAt = DateTime.Now };
        //_context.ChatMessages.Add(message);
        //await _context.SaveChangesAsync();

        // Send the message to the intended recipient
        await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", messageText, senderId);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // Log the user's disconnection
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            // Optional: Notify other users that this user has disconnected
            await Clients.All.SendAsync("UserDisconnected", userId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}
