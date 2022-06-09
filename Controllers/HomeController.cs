using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using MvcMovie.Class;

namespace MvcMovie.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Index()
    {
        TMDBMovie[] tmdbMovie = new TMDBMovie[5];
        for(int i = 0; i < tmdbMovie.Length; i++)
        {
            tmdbMovie[i] = new TMDBMovie();
            tmdbMovie[i].searchMovieById(51533);
        }

        var movieList = new List<Movie>()
        {
            new Movie{
                MovieTitle=tmdbMovie[0].MovieTitle
            },
            new Movie{
                MovieTitle=tmdbMovie[1].MovieTitle
            }
        };

        return View(movieList);
    }

    public async Task<IActionResult> MyView()
    {
        UserInfoController userinfo = new UserInfoController(_context,_hostingEnvironment);
        int id;
        int.TryParse(HttpContext.Session.GetString("user"),out id);

        return View(await userinfo.Details1(id));
    }

/*     public IActionResult UserInfo()
    {
        return View();
    } */
    public async Task<IActionResult> OtherView()
    {
        int userid;
        int.TryParse(HttpContext.Session.GetString("user"),out userid);
        AttentionController attention = new AttentionController(_context);
        ViewData["attention"] = attention.IsAttention1(11,userid);

        UserInfoController userinfo = new UserInfoController(_context,_hostingEnvironment);
        return View(await userinfo.Details1(11));
        // return View();

    }
<<<<<<< HEAD
=======
    public IActionResult MyView1()
    {
        return View();
    }
    public IActionResult a(int? id)
    {
        return RedirectToAction("UserInfo","Edit",id);
    }
>>>>>>> origin/master

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    //     public async Task<IActionResult> Search(string MovieInfo,[Bind("MovieId")] Movie movie)
    //     {
    //         if (ModelState.IsValid)
    //         {
    //             SearchTMDBMovies searchMovie = new SearchTMDBMovies();
    //             searchMovie.Search(MovieInfo);
    //             movie.MovieTitle = searchMovie.GetSearchMoives;
    //             await _context.SaveChangesAsync();
    //             return RedirectToAction(nameof(Index));
    //         }
    //         return View(movie);
    //     }

}