using Microsoft.AspNetCore.Mvc;
namespace MvcMovie.Controllers {
    public class CommunityController: Controller {
        public IActionResult Index() {
            return View();
        }
    }
}