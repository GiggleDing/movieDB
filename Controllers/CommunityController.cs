using Microsoft.AspNetCore.Mvc;
namespace MvcMovie.Controllers {
    public class CommunityController: Controller {

         private readonly MvcAttentionContext _context;
         
        public CommunityController(MvcAttentionContext context)
        {
            _context = context;
        }
        public IActionResult Index() {
            var userid =  from _userinfo in _context.UserInfo
            select _userinfo;   
            var commendId = userid.ToList();



            return View(commendId);
        }
    }
}