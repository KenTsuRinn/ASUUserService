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

        public string DbPath { get; }

        public ApplicationDbContext()
        {
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo exeDirInfo = new DirectoryInfo(exeDir);
            string projectDir = exeDirInfo.Parent.Parent.FullName;
            DbPath = $@"{projectDir}\MigrationDb.sqlite3";

            this.Database.EnsureCreated();
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
