using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    public class BlackIPRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public BlackIPRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _dbContextOptions = options;
        }

        public IList<string> GetBlackIPList()
        {
            using ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);

            return context.BlackIPs.Select(s => s.IP).ToList();
        }
    }
}
