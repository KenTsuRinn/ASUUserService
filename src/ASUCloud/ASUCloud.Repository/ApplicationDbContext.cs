using ASUCloud.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Repository
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<BlackIP> BlackIPs { get; set; }
        public DbSet<BlackUser> BlackUsers { get; set; }

        private string _connectionString { get; set; }

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(_connectionString);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BlackIPConfiguration());
            modelBuilder.ApplyConfiguration(new BlackUserConfiguration());
        }
    }
}
