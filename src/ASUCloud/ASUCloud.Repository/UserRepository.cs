using ASUCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Guid CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.ID;
        }

        public User? Find(string username, string email)
        {
            return _context.Users.Where(s => s.Name == username && s.Email == email).SingleOrDefault();
        }

        public User? FindById(Guid guid)
        {
            return _context.Users.Where(s => s.ID == guid).SingleOrDefault();
        }
    }
}
