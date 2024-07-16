using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository.IntegratedTest
{
    [TestClass]
    public class BlackIPTest
    {
        private ApplicationDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            string connectionString = $"Data Source = test_{Guid.NewGuid()}.sqlite3";
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlite(connectionString: connectionString)
               .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateBlackIP()
        {
            _context.BlackIPs.Add(new Model.BlackIP
            {
                ID = Guid.NewGuid(),
                IP = "127.0.0.1",
                Reason = "just for test"

            });
            _context.SaveChanges();

            int count = _context.BlackIPs
                .Where(s => s.ID != null)
                .Count();

            Assert.IsTrue(count > 0);
        }

    }
}
