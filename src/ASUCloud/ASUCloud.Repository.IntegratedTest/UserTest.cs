using Microsoft.EntityFrameworkCore;

namespace ASUCloud.Repository.IntegratedTest
{
    [TestClass]
    public class UserTest
    {
        private ApplicationDbContext _context;
        private string _connectionString;

        [TestInitialize]
        public void Initialize()
        {
            string connectionString = $"Data Source = test_{Guid.NewGuid()}.sqlite3";
            _connectionString = connectionString;
            _context = new ApplicationDbContext(connectionString);
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateUser()
        {
            _context.Users.Add(new User
            {
                ID = Guid.NewGuid(),
                Name = "Test",
                Email = "Test@gmail.com",
                Password = "password",
                HasVerified = false,
                Icon = "icon"
            });
            _context.SaveChanges();

        }

        [TestMethod]
        public void UpdateUser()
        {
            _context.Users.Add(new User
            {
                ID = Guid.NewGuid(),
                Name = "Test",
                Email = "Test@gmail.com",
                Password = "password",
                HasVerified = false,
                Icon = "icon"
            });
            _context.SaveChanges();

            User? user = _context.Users.Where(s => s.ID != null).FirstOrDefault();
            user.Name = "Test1111";
            user.Email = "t4ew@gmai.com";
            _context.SaveChanges();
        }

        [TestMethod]
        public void CheckNameEmailUniqueIndexTest()
        {

            _context.Users.Add(new User
            {
                ID = Guid.NewGuid(),
                Name = "Test1",
                Email = "Test1@gmail.com",
                Password = "password1",
                HasVerified = false,
                Icon = "icon1"
            });
            _context.Users.Add(new User
            {
                ID = Guid.NewGuid(),
                Name = "Test1",
                Email = "Test1@gmail.com",
                Password = "password",
                HasVerified = false,
                Icon = "icon"
            });


            DbUpdateException ex = Assert.ThrowsException<DbUpdateException>(() =>
            {
                _context.SaveChanges();
            });

            Assert.AreEqual("SQLite Error 19: 'UNIQUE constraint failed: user.name, user.email'.", ex.InnerException.Message);

        }
    }
}