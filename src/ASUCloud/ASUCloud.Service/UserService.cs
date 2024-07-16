using ASUCloud.Model;
using ASUCloud.Repository;

namespace ASUCloud.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool CreateUser(User user)
        {
            //重複登録は防止する
            User? existed = _userRepository.Find(user.Name, user.Email);
            if (existed != null)
            {
                throw new ASUCloudException(ErrorCode.SERVER_ERROR_BUSINESS, ErrorMessage.INSERT_DUPLICATE_USER);
            }

            Guid id = _userRepository.CreateUser(user);
            return id != default(Guid);
        }

        public User GetUserById(Guid id)
        {
            return _userRepository.FindById(id);
        }

    }
}
