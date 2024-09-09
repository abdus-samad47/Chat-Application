using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Models;
using Real_Time_Chat_Application.Models.DTOs;

namespace Real_Time_Chat_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly ChatMessageRepository _repository;
        private readonly IMapper _mapper;

        public ChatMessagesController(ChatMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ChatMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessageDTO>>> GetChatMessages()
        {
            var messages = await _repository.GetAllChatMessagesAsync();
            var messageDTOs = _mapper.Map<IEnumerable<ChatMessageDTO>>(messages);
            return Ok(messageDTOs);
        }

        // GET: api/ChatMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessageDTO>> GetChatMessage(int id)
        {
            var message = await _repository.GetChatMessageByIdAsync(id);
            if (message == null) return NotFound();
            var messageDTO = _mapper.Map<ChatMessageDTO>(message);
            return Ok(messageDTO);
        }

        // POST: api/ChatMessages
        [HttpPost]
        public async Task<ActionResult<ChatMessageDTO>> PostChatMessage(CreateChatMessageDTO createChatMessageDTO)
        {
            var message = _mapper.Map<ChatMessage>(createChatMessageDTO);
            await _repository.AddChatMessageAsync(message);
            var messageDTO = _mapper.Map<ChatMessageDTO>(message);
            return CreatedAtAction(nameof(GetChatMessage), new { id = message.Id }, messageDTO);
        }


        // PUT: api/ChatMessages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatMessage(int id, UpdateChatMessageDTO updateChatMessageDTO)
        {
            await _repository.UpdateChatMessageAsync(id, updateChatMessageDTO);
            return NoContent();
        }

        // DELETE: api/ChatMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatMessage(int id)
        {
            await _repository.DeleteChatMessageAsync(id);
            return NoContent();
        }
    }
}
