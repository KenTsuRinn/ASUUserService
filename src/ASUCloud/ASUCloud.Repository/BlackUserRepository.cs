using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    public class BlackUserRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public BlackUserRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _dbContextOptions = options;
        }

        public IList<string> GetBlackUserIdList()
        {
            using ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);

            return context.BlackUsers.Select(s => s.UserID.ToString()).ToList();
        }
    }
}
