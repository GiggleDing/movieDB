using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
namespace MvcMovie.Controllers;
using Microsoft.EntityFrameworkCore;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MvcAttentionContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public HomeController(ILogger<HomeController> logger, MvcAttentionContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public async Task<IActionResult> MyView()
    {
        UserInfoController userinfo = new UserInfoController(_context,_hostingEnvironment);
        int id;
        int.TryParse(HttpContext.Session.GetString("user"),out id);
        UserInfo user = userinfo.Details1(id);
        return View(user);
    }

    public IActionResult UserInfo()
    {
        return View();
    }
    public IActionResult OtherView()
    {
        int userid;
        int.TryParse(HttpContext.Session.GetString("user"),out userid);
        AttentionController attention = new AttentionController(_context);
        ViewData["attention"] = attention.IsAttention1(99,userid);
        return View();

    }
    public IActionResult MyView1()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
