using ASUCloud.Model;
using ASUCloud.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service.IntegratedTest
{
    [TestClass]
    public class BlackUserCacheTest
    {
        private ApplicationDbContext _context;
        private BlackUserRepository _blackUserRepository;
        private BlackUserCache _blackUserCache;

        [TestInitialize]
        public void Initialize()
        {
            string connectionString = $"Data Source = test_{Guid.NewGuid()}.sqlite3";
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlite(connectionString: connectionString)
               .Options;

            ApplicationDbContext context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            _context = context;

            BlackUserRepository blackIPRepository = new BlackUserRepository(options);
            _blackUserRepository = blackIPRepository;

            ServiceCollection services = new ServiceCollection();
            services.AddMemoryCache();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IMemoryCache? memoryCache = serviceProvider.GetService<IMemoryCache>();

            _blackUserCache = new BlackUserCache(memoryCache, blackIPRepository);

        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _blackUserCache.Reset();
        }

        [TestMethod]
        public void TestGetEmptyBlackUserList()
        {
            IList<string> result = _blackUserCache.GetBlackUserList();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetBlackUserList()
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

            IList<string> result = _blackUserCache.GetBlackUserList();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
