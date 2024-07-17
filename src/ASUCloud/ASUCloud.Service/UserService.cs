using ASUCloud.Model;
using ASUCloud.Repository;
using Serilog.Events;

namespace ASUCloud.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly EventBus _eventBus;

        public UserService(UserRepository userRepository, EventBus eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public User? FindUser(User user)
        {
            return _userRepository.FindByPwd(user.Name, user.Email, user.Password);
        }

        public User CreateUser(User user)
        {
            //重複登録は防止する
            User? existed = _userRepository.Find(user.Name, user.Email);
            if (existed != null)
            {
                _eventBus.Publish<EventArgs<LogEvent>>(new EventArgs<LogEvent>(
                    new LogEvent
                    {

                        Level = LogEventLevel.Error,
                        Message = ErrorMessage.INSERT_DUPLICATE_USER
                    }));
                throw new ASUCloudException(ErrorCode.SERVER_ERROR_BUSINESS, ErrorMessage.INSERT_DUPLICATE_USER);
            }

            return _userRepository.CreateUser(user);
        }

        public User GetUserById(Guid id)
        {
            return _userRepository.FindById(id);
        }

    }
}
