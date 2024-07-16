using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    public class BlackIPRepository
    {
        private readonly ApplicationDbContext _context;
        public BlackIPRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<string> GetBlackIPList()
        {
            return _context.BlackIPs.Select(s => s.IP).ToList();
        }
    }
}
