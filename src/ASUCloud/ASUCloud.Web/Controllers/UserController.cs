using ASUCloud.Service;
using ASUCloud.Web.Middleware;
using ASUCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASUCloud.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;
        private readonly SecurityService _securityService;

        public UserController(ILogger<UserController> logger, UserService userService, SecurityService securityService)
        {
            _logger = logger;
            _userService = userService;
            _securityService = securityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogDebug("test for webapp");
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel loginUser)
        {
            throw new NotImplementedException();
            ModelState.Clear();
            if (!TryValidateModel(loginUser))
            {
                return View("Index", loginUser);
            }

            Model.User user = loginUser.ToDomainUser();
            user.Password = _securityService.GeneratePasswordHashPBKDF2(user.Password);

            Model.User? found = _userService.FindUser(user);

            if (found == null)
                return View("Index", loginUser);

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterSubmit(RegisterUserViewModel registerUser)
        {
            ModelState.Clear();
            if (!TryValidateModel(registerUser))
            {
                return View("Register", registerUser);
            }

            Model.User user = registerUser.ToDomainUser();
            user.Password = _securityService.GeneratePasswordHashPBKDF2(user.Password);
            Model.User created = _userService.CreateUser(user);

            return View("Details", created.ToViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
