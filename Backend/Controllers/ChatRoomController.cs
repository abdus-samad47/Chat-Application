using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Models;
using Real_Time_Chat_Application.Models.DTOs;
using Real_Time_Chat_Application.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Real_Time_Chat_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRooms()
        {
            var chatRooms = await _context.ChatRooms.ToListAsync();
            return Ok(chatRooms);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ChatRoom>> GetChatRoom(int id)
        //{
        //    var chatRoom = await _repository.GetByIdAsync(id);
        //    if (chatRoom == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(chatRoom);
        //}

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateChatRoom([FromBody] CreateChatRoomDTO createChatRoomDTO)
        {
            if (createChatRoomDTO == null || createChatRoomDTO.Users == null || !createChatRoomDTO.Users.Any())
            {
                return BadRequest("ChatRoom data is invalid.");
            }

            // Create the ChatRoom entity
            var chatRoom = new ChatRoom
            {
                RoomName = createChatRoomDTO.RoomName,
                CreatedBy = createChatRoomDTO.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                //ChatRoomUsers = createChatRoomDTO.UserIds.Select(userId => new ChatRoomUser
                //{
                //    UserId = userId,
                //    JoinedAt = DateTime.UtcNow,
                //    Role = "Admin" // Set a default role
                //}).ToList()
            };
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            var chatRoomUsers = createChatRoomDTO.Users.Select(UserId => new ChatRoomUser
            {
                ChatRoomId = chatRoom.RoomId,
                UserId = UserId,
                JoinedAt = DateTime.UtcNow,
                Role = "Member"
            }).ToList();

            _context.ChatRoomUsers.AddRange(chatRoomUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChatRooms), new { id = chatRoom.RoomId }, chatRoom);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateChatRoom(int id, [FromBody] ChatRoom chatRoom)
        //{
        //    if (id != chatRoom.RoomId)
        //    {
        //        return BadRequest();
        //    }

        //    await _repository.UpdateAsync(chatRoom);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteChatRoom(int id)
        //{
        //    await _repository.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
