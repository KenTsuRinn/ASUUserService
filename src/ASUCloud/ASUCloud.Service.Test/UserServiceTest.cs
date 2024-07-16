using ASUCloud.Model;
using ASUCloud.Repository;
using Microsoft.EntityFrameworkCore;

namespace ASUCloud.Service.IntegratedTest
{
    [TestClass]
    public class UserServiceTest
    {
        private ApplicationDbContext _context;
        private UserRepository _userRepository;
        private UserService _userService;

        [TestInitialize]
        public void Initialize()
        {
            string connectionString = $"Data Source = test_{Guid.NewGuid()}.sqlite3";
            ApplicationDbContext context = new ApplicationDbContext(connectionString);
            context.Database.EnsureCreated();
            _context = context;

            UserRepository userRepository = new UserRepository(context);
            _userRepository = userRepository;
            _userService = new UserService(userRepository, EventBus.Instance.Subscribe());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void TestCreateUser()
        {
            string name = "Test";
            string email = "test@gmail.com";
            bool result = _userService.CreateUser(new Model.User
            {
                Name = name,
                Email = email,
                Icon = "jfoiewjewofew",
                HasVerified = false,
                Password = "fjioewjfoew"
            });

            Assert.IsTrue(result);

            Model.User? user = _userRepository.Find(name, email);
            Assert.IsNotNull(user);

        }

        [TestMethod]
        public void TestCreateDuplicateUser()
        {

            string name = "Test";
            string email = "test@gmail.com";

            bool result = _userService.CreateUser(new Model.User
            {
                Name = name,
                Email = email,
                Icon = "jfoiewjewofew",
                HasVerified = false,
                Password = "fjioewjfoew"
            });

            Assert.IsTrue(result);

            ASUCloudException ex = Assert.ThrowsException<ASUCloudException>(() =>
            {
                _userService.CreateUser(new Model.User
                {
                    Name = name,
                    Email = email,
                    Icon = "jfoiewjewofew",
                    HasVerified = false,
                    Password = "fjioewjfoew"
                });
            });

            Assert.IsTrue(ex.ErrorCode == ErrorCode.SERVER_ERROR_BUSINESS);
            Assert.IsTrue(ex.Message == ErrorMessage.INSERT_DUPLICATE_USER);
        }

    }
}