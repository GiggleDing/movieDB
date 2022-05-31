using Microsoft.AspNetCore.Mvc;
namespace MvcMovie.Controllers {
    public class HomePageController: Controller {
        public IActionResult Index() {
            return View();
        }
    }
}