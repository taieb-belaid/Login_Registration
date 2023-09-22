using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginRegistration.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    //____________SUCCESS_PAGE______
    [HttpGet("success")]
    public IActionResult Success()
    {
        return View();
    }
    //___________REGISTER_POST________
    [HttpPost("/user/register")]
    public IActionResult Register(User newUser)
    {
        if (ModelState.IsValid)
        {
            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "Email already taken !");
                return View("Index");
            }
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();  
            return RedirectToAction("success");
        }

        return View("Index");
    }
    //___________LogUser_________
    [HttpPost("/user/login")]
    public IActionResult Login(LogUser log)
    {
        if(ModelState.IsValid)
        {
        User? userInDb = _context.Users.FirstOrDefault(l => l.Email == log.LogEmail);
            if(userInDb == null)
            {
                ModelState.AddModelError("LogEmail","Email or Password wrong");
            }
        PasswordHasher <LogUser> Hasher = new PasswordHasher<LogUser>();
        var result = Hasher.VerifyHashedPassword(log, userInDb.Password,log.LogPassword);
        Console.WriteLine(result);
            if(result == 0)
            {
                ModelState.AddModelError("LogEmail","Something went wrong");
                return View("Index");
            }
        return View("success");   
        }
        return View("Index");
    }

    //___________Logout___________
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        return View("Index");
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
