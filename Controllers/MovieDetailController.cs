using Microsoft.AspNetCore.Mvc;
using MvcMovie.Class;
namespace MvcMovie.Controllers {
    public class MovieDetailController: Controller {
        public IActionResult Index(int MovieId) {
            TMDBMovie movie = new TMDBMovie();
            movie.searchMovieById(MovieId);
            ViewData["movie"] = movie;
            return View();
        }
    }
}