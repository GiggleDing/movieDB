using Microsoft.AspNetCore.Mvc;
using MvcMovie.Class;
using MvcMovie.Models;
namespace MvcMovie.Controllers {
    public class MovieDetailController: Controller {

        private readonly MvcCollectionContext _context;
        public MovieDetailController(MvcCollectionContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> CollectClick(int id)
        {

            if (ModelState.IsValid)
            {
                int userid;
                if(int.TryParse(HttpContext.Session.GetString("user"),out userid))
                {
                    Collection collection = new Collection();
                    collection.UserID = userid;
                    collection.MovieID = id;
                    _context.Add(collection);
                    await _context.SaveChangesAsync();
                    return Content("收藏成功！");
                }
                return Content("收藏失败！");
            }else{
                    return Content("收藏失败！");
            }

        }




        public IActionResult Index(int MovieId) {
            TMDBMovie movie = new TMDBMovie();
            movie.searchMovieById(MovieId);
            ViewData["movie"] = movie;
            return View();
        }
    }
}