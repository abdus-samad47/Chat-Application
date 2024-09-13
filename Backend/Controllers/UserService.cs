//using Real_Time_Chat_Application.Data;
//using Real_Time_Chat_Application.Models;
//using Real_Time_Chat_Application.Models.DTOs;

//namespace Real_Time_Chat_Application.Controllers
//{
//    public class UserService
//    {
//        private readonly UserRepository _userRepository;

//        public UserService(UserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public IEnumerable<UserDTO> GetAllUsers()
//        {
//            return _userRepository.GetAllUsers();
//        }

//        public UserDTO GetUserById(int id)
//        {
//            return _userRepository.GetUserById(id);
//        }

//        public void CreateUser(CreateUserDTO createUserDTO)
//        {
//            if (string.IsNullOrEmpty(createUserDTO.PasswordHash))
//            {
//                throw new ArgumentException("Password is required");
//            }
//            _userRepository.AddUser(createUserDTO);
//        }

//        public void UpdateUser(int id, UpdateUserDTO updateUserDTO)
//        {
//            _userRepository.UpdateUser(id, updateUserDTO);
//        }

//        public void DeleteUser(int id)
//        {
//            _userRepository.DeleteUser(id);
//        }

//        internal async Task GetUserByIdAsync(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
