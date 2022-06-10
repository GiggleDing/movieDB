using Microsoft.AspNetCore.Mvc;
using MvcMovie.Class;
using MvcMovie.Models;
namespace MvcMovie.Controllers
{
    public class MovieDetailController : Controller
    {

        private readonly MvcCollectionContext _context;
        public MovieDetailController(MvcCollectionContext context)
        {
            _context = context;
        }

        //判断用户是否收藏
        public string IsCollection(int userid, int movieId){
            bool isCollect = _context.Collection.Any(a => a.UserID == userid && a.MovieID == movieId );
            if(isCollect){
                return "已收藏";
            }else{
                return "收藏";
            }
        }

        public async Task<IActionResult> CollectClick(int id)
        {

            if (ModelState.IsValid)
            {
                int userid;
                if (int.TryParse(HttpContext.Session.GetString("user"), out userid))
                {
                    Collection collection = new Collection();
                    collection.UserID = userid;
                    collection.MovieID = id;
                    _context.Add(collection);
                    await _context.SaveChangesAsync();
                    MovieDetailController collectionController = new MovieDetailController(_context);
                     ViewData["collection"] = collectionController.IsCollection(userid,id);
                    return RedirectToAction("Index",routeValues:new{MovieId=id});
                }
                return RedirectToAction("Index",routeValues:new{MovieId=id});
            }else{
                    return RedirectToAction("Index",routeValues:new{MovieId=id});
            }

        }




        public IActionResult Index(int MovieId)
        {
            TMDBMovie movie = new TMDBMovie();
            movie.searchMovieById(MovieId);
            ViewData["movie"] = movie;
            int userid;
            int.TryParse(HttpContext.Session.GetString("user"), out userid);
            MovieDetailController collection = new MovieDetailController(_context);
            ViewData["collection"] = collection.IsCollection(userid,MovieId);
            return View();
        }
    }
}