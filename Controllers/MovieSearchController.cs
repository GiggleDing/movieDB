using Microsoft.AspNetCore.Mvc;
using MvcMovie.Class;
namespace MvcMovie.Controllers {
    public class MovieSearchController: Controller {
        public IActionResult Index(string searchString) {
            SearchTMDBMovies movies = new SearchTMDBMovies();
            movies.Search(searchString);
            ViewData["searchResult"] = movies;
            return View();
        }
    }
}