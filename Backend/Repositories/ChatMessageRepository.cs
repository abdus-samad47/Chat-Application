//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Real_Time_Chat_Application.Models;
//using Real_Time_Chat_Application.Models.DTOs;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Real_Time_Chat_Application.Data
//{
//    public class ChatMessageRepository
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IMapper _mapper;

//        public ChatMessageRepository(ApplicationDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<ChatMessageDTO>> GetAllChatMessagesAsync()
//        {
//            var messages = await _context.ChatMessages.ToListAsync();
//            return _mapper.Map<IEnumerable<ChatMessageDTO>>(messages);
//        }

//        public async Task<ChatMessageDTO?> GetChatMessageByIdAsync(int id)
//        {
//            var message = await _context.ChatMessages.FindAsync(id);
//            return message == null ? null : _mapper.Map<ChatMessageDTO>(message);
//        }

//        public async Task AddChatMessageAsync(ChatMessage message)
//        {
//            _context.ChatMessages.Add(message);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateChatMessageAsync(int id, UpdateChatMessageDTO updateChatMessageDTO)
//        {
//            var message = await _context.ChatMessages.FindAsync(id);
//            if (message == null) return;

//            _mapper.Map(updateChatMessageDTO, message);
//            _context.ChatMessages.Update(message);
//            await _context.SaveChangesAsync();
//        }

//        public async Task DeleteChatMessageAsync(int id)
//        {
//            var message = await _context.ChatMessages.FindAsync(id);
//            if (message == null) return;

//            _context.ChatMessages.Remove(message);
//            await _context.SaveChangesAsync();
//        }
//    }
//}
