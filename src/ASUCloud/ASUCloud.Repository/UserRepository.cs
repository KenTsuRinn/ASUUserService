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

        public User CreateUser(User user)
        {
            using ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public User? FindByPwd(string username, string email, string password)
        {
            using ApplicationDbContext context = new ApplicationDbContext(_dbContextOptions);
            return context.Users.Where(s => s.Name == username && s.Email == email && s.Password == password).SingleOrDefault();
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
