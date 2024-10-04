using Microsoft.EntityFrameworkCore;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Repositories
{
    public class ChatRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatRoom>> GetAllAsync()
        {
            return await _context.ChatRooms
                .Include(cr => cr.Messages)
                .Include(cr => cr.ChatRoomUsers)
                .ToListAsync();
        }

        public async Task<ChatRoom> GetByIdAsync(int id)
        {
            return await _context.ChatRooms
                .Include(cr => cr.Messages)
                .Include(cr => cr.ChatRoomUsers)
                .FirstOrDefaultAsync(cr => cr.RoomId == id);
        }

        public async Task CreateAsync(ChatRoom chatRoom)
        {
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChatRoom chatRoom)
        {
            _context.ChatRooms.Update(chatRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chatRoom = await _context.ChatRooms.FindAsync(id);
            if (chatRoom != null)
            {
                _context.ChatRooms.Remove(chatRoom);
                await _context.SaveChangesAsync();
            }
        }
    }
}
