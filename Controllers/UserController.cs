using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Models;
using Real_Time_Chat_Application.Models.DTOs;
using Real_Time_Chat_Application.Utility;

namespace Real_Time_Chat_Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VerifyUserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDTO>(user));
        }

        //[HttpPost("Verify")]
        //public async Task<ActionResult<UserDTO>> Verify(VerifyUserDTO verifyuserDTO)
        //{
        //    // Find user by name
        //    var user = await _context.Users
        //        .SingleOrDefaultAsync(u => u.Username == verifyuserDTO.Username);

        //    if (user == null)
        //    {
        //        return Unauthorized("Invalid username or password.");
        //    }

        //    // Verify password
        //    var hashedPassword = Hashing.hashPassword(verifyuserDTO.Password, user.Salt);
        //    if (user.PasswordHash != hashedPassword)
        //    {
        //        return Unauthorized("Invalid username or password.");
        //    }

        //    // If successful, return user data (or a token if using authentication tokens)
        //    return Ok(_mapper.Map<UserDTO>(user));
        //}


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(CreateUserDTO createUserDTO)
        {
            var salt = Hashing.Salt();
            var password = createUserDTO.PasswordHash;
            var hashingPassowrd = Hashing.hashPassword(password, salt);
            createUserDTO.PasswordHash = hashingPassowrd;
            var user = _mapper.Map<User>(createUserDTO);
            user.Salt = salt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, _mapper.Map<UserDTO>(user));
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Map updated fields to the existing user
            _mapper.Map(updateUserDTO, user);

            // Mark the entity as modified
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
