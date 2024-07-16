using ASUCloud.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    public class UserRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public UserRepository(DbContextOptions<ApplicationDbContext> options)
        {
            _dbContextOptions = options;
        }

        public Guid CreateUser(User user)
        {
            using ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);
            context.Users.Add(user);
            context.SaveChanges();
            return user.ID;
        }

        public User? Find(string username, string email)
        {
            using ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);

            return context.Users.Where(s => s.Name == username && s.Email == email).SingleOrDefault();
        }

        public User? FindById(Guid guid)
        {
            using ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);

            return context.Users.Where(s => s.ID == guid).SingleOrDefault();
        }
    }
}
