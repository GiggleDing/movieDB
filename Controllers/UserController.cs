using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MvcMovie.Models;
using System.Data;


namespace MvcMovie.Controllers
{
    public class UserController : Controller
    {
        
        private readonly MvcUserContext _context;

        public UserController(MvcUserContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            //ViewBag.Code=HttpContext.Session.GetString("user");
            return View(await _context.User.ToListAsync());
            //return Content(ViewBag.Code);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.[Bind("Id,UserName,UserPwd")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,UserPwd")] User user)
        {
            ViewData["error"] = "";
            if (ModelState.IsValid)
            {
                //检查该用户名是否已经被注册过
                var userinfo = _context.User.FromSqlRaw(string.Format("select * from User where UserName='{0}'", user.UserName)).ToList();
                if (userinfo.Count == 0)
                {
                    //对密码用MD5加密
                    user.UserPwd = Encrypt.ByMd5_1(user.UserPwd);
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index)); 
                    ViewData["success"] = "注册成功！";
                    return View();
                }
                else
                {
                    ViewData["error"] = "该用户已经被注册过了";
                    return View();
                }
                    
            }
            ViewData["error"] = "用户名或密码长度不合法";
            return View();
        }

        // GET: User/Login
        public ActionResult Login(int? id)
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            ViewData["error"] = "";
            if(ModelState.IsValid){

                var userinfo0 = _context.User.FromSqlRaw(string.Format("select * from User where UserName='{0}'", user.UserName)).ToList();
                // 检查用户是否存在
                if (userinfo0.Count == 0)
                {
                    ViewData["error"] = "该用户未注册过";
                    return View();                   
                }else
                {
                     //检查用户和密码是否对应
                    var pwdMD5 = Encrypt.ByMd5_1(user.UserPwd);
                    if (userinfo0[0].UserPwd == pwdMD5)
                    {
                        //把用户id存进session中
                        HttpContext.Session.SetString("user",userinfo0[0].Id.ToString());
                        return RedirectToAction("Index");
                    }else
                    {
                        ViewData["error"] = "密码输入错误";
                        return View();
                    }
                }
            }
            ViewData["error"] = "用户名或密码长度不合法";
            return View();
 
        }


        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.User.FindAsync(id);
            _context.User.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
