using ASUCloud.Service;
using ASUCloud.Web.Middleware;
using ASUCloud.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASUCloud.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;
        private readonly SecurityService _securityService;
        private readonly TokenService _tokenService;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public UserController(
            IConfiguration configuration,
            ILogger<UserController> logger,
            UserService userService,
            SecurityService securityService,
            TokenService tokenService)
        {
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
            _securityService = securityService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogDebug("test for webapp");
            return View();
        }

        [HttpPost]
        public IActionResult TokenLogin([FromBody] LoginUser loginUser)
        {
            // ここでデータベース照合等のユーザー認証を行う
            // サンプルは、メールアドレスとパスワードが一致する場合に成功とします
            if (loginUser.Email == "admin@sample.com" && loginUser.Password == "admin")
            {
                var token = _tokenService.GenerateToken(
                    _configuration["Jwt:Key"],
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    loginUser.Name,
                    loginUser.Email);

                return Ok(new { token = token });
            }
            return Unauthorized();

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

        [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
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
