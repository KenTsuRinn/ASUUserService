using ASUCloud.Service;
using ASUCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASUCloud.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
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
            ModelState.Clear();
            if (!TryValidateModel(loginUser))
            {
                return View("Index", loginUser);
            }

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

            return View();
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
