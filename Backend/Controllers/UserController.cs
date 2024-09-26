using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Entities;
using Real_Time_Chat_Application.Models.DTOs;
using Real_Time_Chat_Application.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Real_Time_Chat_Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _secretKey;

        public UsersController(ApplicationDbContext context, IMapper mapper, string secretKey)
        {
            _context = context;
            _mapper = mapper;
            _secretKey = secretKey;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }

        // GET: api/Users/5
        [Authorize]
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

        [HttpPost("Verify")]
        public async Task<ActionResult<UserDTO>> Verify(VerifyUserDTO verifyUserDTO)
        {
            // Find user by name
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Username == verifyUserDTO.Username);

            if (user == null)
            {
                return BadRequest("Invalid Username or Password");
            }

            var hashedPassword = Hashing.hashPassword(verifyUserDTO.Password, user.Salt);
            var verify = Hashing.VerifyPassword(verifyUserDTO.Password, user.PasswordHash, user.Salt);
            var token = GenerateJwtToken(user);

            if (verify)
            {
                var userDto = _mapper.Map<UserDTO>(user);
                return Ok(new { UserDTO = userDto, Token = token });
            }
            else
            {
                return BadRequest("Invalid Username or Password");
            }
        }
        private string GenerateJwtToken(User user)
        {
            // Define claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                //new Claim(ClaimTypes.Name, user.Username)
            };

            // Create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("We_Connect_Private_Limited_12345"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(CreateUserDTO createUserDTO)
        {
            // Check if a user with the same email already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == createUserDTO.Email);

            if (existingUser != null)
            {
                return Conflict("User with the same email already exists.");
            }

            var salt = Hashing.Salt();
            var password = createUserDTO.PasswordHash;
            var hashingPassowrd = Hashing.hashPassword(password, salt);
            createUserDTO.PasswordHash = hashingPassowrd;
            var user = _mapper.Map<User>(createUserDTO);
            user.Salt = salt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserDTO>(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, _mapper.Map<UserDTO>(user));
        }
        

        [Authorize]
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserDTO updateUserDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(updateUserDTO, user);

            // Mark the entity as modified
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5
        [Authorize]
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
