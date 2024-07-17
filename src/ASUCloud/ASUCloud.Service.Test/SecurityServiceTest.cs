using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service.IntegratedTest
{
    [TestClass]
    public class SecurityServiceTest
    {
        private SecurityService _securityService;
        [TestInitialize]
        public void Initialize()
        {
            SecurityService service = new SecurityService();
            _securityService = service;
        }

        [TestMethod]
        public void TestGeneratePasswordHashPBKDF2()
        {
            string hashPwd = _securityService.GeneratePasswordHashPBKDF2("password");
            Assert.IsNotNull(hashPwd);
            Assert.IsFalse(string.IsNullOrWhiteSpace(hashPwd));
        }
    }
}
