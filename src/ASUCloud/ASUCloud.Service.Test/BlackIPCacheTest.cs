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
    public class BlackIPCacheTest
    {
        private BlackIPRepository _blackIPRepository;
        private BlackIPCache _blackIPCache;
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            string connectionString = $"Data Source = test_{Guid.NewGuid()}.sqlite3";

            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlite(connectionString: connectionString)
               .Options;

            _context = new ApplicationDbContext(options);

            BlackIPRepository blackIPRepository = new BlackIPRepository(options);
            _blackIPRepository = blackIPRepository;

            ServiceCollection services = new ServiceCollection();
            services.AddMemoryCache();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IMemoryCache? memoryCache = serviceProvider.GetService<IMemoryCache>();

            _blackIPCache = new BlackIPCache(memoryCache, blackIPRepository);

        }

        [TestCleanup]
        public void Cleanup()
        {
            _blackIPCache.Reset();
        }

        [TestMethod]
        public void TestGetEmptyBlackIpList()
        {
            IList<string> result = _blackIPCache.GetBlackIPList();
            Assert.IsNotNull(result);   
        }

        [TestMethod]
        public void TestGetBlackIpList()
        {
            _context.BlackIPs.Add(new Model.BlackIP
            {
                ID = Guid.NewGuid(),
                IP = "127.0.0.1",
                Reason = "just for test"

            });
            _context.BlackIPs.Add(new Model.BlackIP
            {
                ID = Guid.NewGuid(),
                IP = "127.0.0.2",
                Reason = "just for test"

            });
            _context.SaveChanges();


            IList<string> result = _blackIPCache.GetBlackIPList();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
