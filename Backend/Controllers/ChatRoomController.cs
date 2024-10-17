using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Entities;
using Real_Time_Chat_Application.Models;
using Real_Time_Chat_Application.Models.DTOs;
using Real_Time_Chat_Application.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Real_Time_Chat_Application.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChatRoomController> _logger;

        public ChatRoomController(ApplicationDbContext context, ILogger<ChatRoomController> logger)
        {
            _context = context;
            _logger = logger;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRooms()
        {
            //_logger.LogInformation("Getting all Chat Rooms");
            var chatRooms = await _context.ChatRooms.ToListAsync();
            return Ok(chatRooms);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetUserChatRooms(int userId)
        {
            //_logger.LogInformation("Getting Chat Rooms related to the session user");
            var chatRooms = await _context.ChatRoomUsers
                .Where(cru => cru.UserId == userId)
                .Include(cru => cru.ChatRoom)
                .Select(cru => cru.ChatRoom)
                .ToListAsync();

            if (chatRooms == null || !chatRooms.Any())
            {
                return NotFound("No chat rooms found for this user.");
            }
            return Ok(chatRooms);
        }

        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<IEnumerable<ChatRoomUser>>> GetChatRoomUser(int roomId)
        {
            var users = await _context.ChatRoomUsers
                .Where(cru => cru.ChatRoomId == roomId)
                .Include(cru => cru.User)
                .Select(cru => cru.User)
                .ToListAsync();

            if (users == null || !users.Any())
            {
                return NotFound("No chat rooms found for this user.");
            }
            return Ok(users);
        }

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

            _logger.LogInformation($"{chatRoom.RoomName} chat room created");

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
