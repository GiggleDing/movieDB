using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
namespace MvcMovie.Controllers;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Class;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MvcAttentionContext _context;
    private readonly MvcCollectionContext _context2;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public HomeController(ILogger<HomeController> logger, MvcAttentionContext context, MvcCollectionContext context2)
    {
        _logger = logger;
        _context = context;
        _context2 = context2;

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
        UserInfoController userinfo = new UserInfoController(_context, _hostingEnvironment);
        int id;
        int.TryParse(HttpContext.Session.GetString("user"), out id);

        var movieid = from _Collection in _context2.Collection
                      where _Collection.UserID == id
                      select _Collection.MovieID;

        var idarray = movieid.ToArray();
        TMDBMovie[] movies = new TMDBMovie[idarray.Length];
        for (int i = 0; i < idarray.Length; i++)
        {
            movies[i] = new TMDBMovie();
            movies[i].searchMovieById(idarray[i]);
        }

        var mymovie = movies.ToList();
        ViewData["mymovie"] = mymovie;

        var att = from _Attention in _context.Attention
                      from _UserInfo in _context.UserInfo
                      where _Attention.UserID == id && _UserInfo.UserID == _Attention.AttentionID
                      select _UserInfo;


        var attinfo = att.ToList();
        ViewData["myatt"] = attinfo;

        return View(await userinfo.Details1(id));
    }

    /*     public IActionResult UserInfo()
        {

            return View();
        } */
    public IActionResult OtherView()
    {
        int userid;
        int.TryParse(HttpContext.Session.GetString("user"), out userid);
        AttentionController attention = new AttentionController(_context);
        ViewData["attention"] = attention.IsAttention1(99, userid);
        return View();

    }
    public IActionResult MyView1()
    {
        return View();
    }


    public IActionResult a(int? id)
    {
        return RedirectToAction("UserInfo", "Edit", id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
