using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Real_Time_Chat_Application.Models;
using Real_Time_Chat_Application.Models.DTOs;

namespace Real_Time_Chat_Application.Data
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
    public UserRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
        public IEnumerable<UserDTO> GetAllUsers()
    {
        var users = _context.Users.ToList();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public UserDTO GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        return _mapper.Map<UserDTO>(user);
    }

        public void AddUser(CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrWhiteSpace(createUserDTO.Password))
            {
                throw new ArgumentException("Password is required.");
            }
            // Map CreateUserDTO to User
            var user = _mapper.Map<User>(createUserDTO);

            // Hash the password and set the PasswordHash property
            user.PasswordHash = PasswordHelper.HashPassword(createUserDTO.Password);

            // Add the user to the context and save
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            var user = _context.Users.Find(id);
            if (user == null) return;

            _mapper.Map(updateUserDTO, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
