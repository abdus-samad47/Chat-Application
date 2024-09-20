﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Entities;
using Real_Time_Chat_Application.Models;
using Real_Time_Chat_Application.Models.DTOs;
using System.Security.Claims;

namespace Real_Time_Chat_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChatMessagesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ChatMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessageDTO>>> GetChatMessages()
        {
            var messages = await _context.ChatMessages.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ChatMessageDTO>>(messages));
        }

        // GET: api/ChatMessages/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ChatMessageDTO>> GetChatMessage(int id)
        //{
        //    var message = await _context.ChatMessages.FindAsync(id);

        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(_mapper.Map<ChatMessageDTO>(message));
        //}

        // GET: api/ChatMessages/{receiverId}
        //[HttpGet("{receiverId}")]
        //public async Task<ActionResult<IEnumerable<ChatMessageDTO>>> GetMessagesByReceiverId(int receiverId)
        //{
        //    var messages = await _context.ChatMessages
        //        .Include(m => m.Sender) // Include sender details
        //        .Include(m => m.Receiver) // Include receiver details
        //        .Where(m => m.ReceiverId == receiverId || m.SenderId == receiverId)
        //        .ToListAsync();

        //    var messageDtos = messages.Select(m => new ChatMessageDTO
        //    {
        //        MessageId = m.MessageId,
        //        MessageText = m.MessageText,
        //        SenderUsername = m.Sender.Username, // Assuming you add this to your DTO
        //        ReceiverUsername = m.Receiver.Username // Assuming you add this to your DTO
        //    });

        //    return Ok(messageDtos);
        //}

        [HttpGet("conversation")]
        public async Task<ActionResult<IEnumerable<ChatMessageDTO>>> GetConversation(int receiverId, int senderId)
        {
            var messages = await _context.ChatMessages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                             (m.SenderId == receiverId && m.ReceiverId == senderId))
                .ToListAsync();

            if (messages == null || !messages.Any())
            {
                return NotFound(); 
            }

            return Ok(_mapper.Map<IEnumerable<ChatMessageDTO>>(messages));
        }


        // POST: api/ChatMessages
        [HttpPost]
        public async Task<ActionResult<ChatMessageDTO>> PostChatMessage(CreateChatMessageDTO createChatMessageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = _mapper.Map<ChatMessage>(createChatMessageDTO);

            if (message.ReceiverId != null)
            {
                var receiverExists = await _context.Users.AnyAsync(u => u.UserId == message.ReceiverId);
                if (!receiverExists)
                {
                    return BadRequest("Receiver ID does not exist in the User table.");
                }
            }

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChatMessages), new { id = message.MessageId }, _mapper.Map<ChatMessageDTO>(message));
        }


        // PUT: api/ChatMessages/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutChatMessage(int id, UpdateChatMessageDTO updateChatMessageDTO)
        //{
        //    var message = await _context.ChatMessages.FindAsync(id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    // Map updated fields to the existing user
        //    _mapper.Map(updateChatMessageDTO, message);

        //    // Mark the entity as modified
        //    _context.Entry(message).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // DELETE: api/ChatMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(int id)
        {
            var message = await _context.ChatMessages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            _context.ChatMessages.Remove(message);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
