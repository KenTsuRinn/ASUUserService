using ASUCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository.IntegratedTest
{
    [TestClass]
    public class BlackUserTest
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
        public void CreateBlackUser()
        {
            User user = new User
            {
                ID = Guid.NewGuid(),
                Name = "Test",
                Email = "Test@gmail.com",
                Password = "password",
                HasVerified = false,
                Icon = "icon"
            };
            _context.Users.Add(user);
            _context.SaveChanges();


            _context.BlackUsers.Add(new BlackUser
            {
                ID = Guid.NewGuid(),
                UserID = user.ID,
                Reason = "just for test"
            });
            _context.SaveChanges();

            int count = _context.BlackUsers
                .Where(s => s.ID != null)
                .Count();

            Assert.IsTrue(count == 1);
        }
    }
}
