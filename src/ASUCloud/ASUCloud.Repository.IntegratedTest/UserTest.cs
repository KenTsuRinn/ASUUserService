using Microsoft.EntityFrameworkCore;

namespace ASUCloud.Repository.IntegratedTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateUser()
        {
            using var db = new ApplicationDbContext();

            db.Users.Add(new User
            {
                ID = Guid.NewGuid(),
                Name = "Test",
                Email = "Test@gmail.com",
                Password = "password",
                HasVerified = false,
                Icon = "icon"
            });
            db.SaveChanges();

        }

        [TestMethod]
        public void UpdateUser()
        {
            using var db = new ApplicationDbContext();
            User? user = db.Users.Where(s => s.ID != null).FirstOrDefault();
            user.Name= "Test1111";
            user.Email = "t4ew@gmai.com";
            db.SaveChanges();


        }
    }
}