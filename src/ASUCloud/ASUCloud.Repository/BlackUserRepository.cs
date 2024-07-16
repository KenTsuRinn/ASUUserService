using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    public class BlackUserRepository
    {
        private readonly ApplicationDbContext _context;
        public BlackUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<string> GetBlackUserIdList()
        {
            return _context.BlackUsers.Select(s => s.UserID.ToString()).ToList();
        }
    }
}
