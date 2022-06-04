using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MvcMovie.Models;
using Microsoft.AspNetCore.Builder;


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
            // return(Content(HttpContext.Session.GetString("user")));
            // return View(await _context.User.ToListAsync());
            return View(await _context.User.ToListAsync());

           
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
                    user.UserPwd = Encrypt.ByMd5(user.UserPwd);
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
            ViewData["UserName"] = Request.Cookies["UserName"];
            ViewData["UserPwd"] = Request.Cookies["UserPwd"];
            ViewData["remember"] = Request.Cookies["remember"];
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
                
                //如果当前的用户名是cookie里面的，就直接登录不用校验。
                if(Request.Cookies["UserName"] == user.UserName)
                {
                    HttpContext.Session.SetString("user",userinfo0[0].Id.ToString());
                    HttpContext.Session.SetString("userMame",userinfo0[0].UserName.ToString());
                    if(Request.Form["remember"] == "1")
                    {
                        CookieOptions option = new CookieOptions(); 
                        option.Expires = DateTime.Now.AddDays(7); 
                        Response.Cookies.Append("UserName", user.UserName, option); 
                        Response.Cookies.Append("UserPwd", user.UserPwd, option);
                        Response.Cookies.Append("remember", "1", option); 
                    }
                    return RedirectToAction("MyView","Home");

                }
               
                // 检查用户是否存在
                if (userinfo0.Count == 0)
                {
                    ViewData["error"] = "该用户未注册过";
                    return View();                   
                }
                else
                {
                    //检查用户和密码是否对应
                    var pwdMD5 = Encrypt.ByMd5(user.UserPwd);
                    if (userinfo0[0].UserPwd == pwdMD5)
                    {
                        //把用户id存进session中
                        HttpContext.Session.SetString("user",userinfo0[0].Id.ToString());
                        HttpContext.Session.SetString("userMame",userinfo0[0].UserName.ToString());

                        //选中七天免登录
                        if(Request.Form["remember"] == "1")
                        {
                            //如果变了一个用户，先把之前用户的cookie清除
                            if(Request.Cookies["UserName"] != user.UserName)
                            {
                                Response.Cookies.Delete("UserName");
                                Response.Cookies.Delete("UserPwd");
                                Response.Cookies.Delete("remember"); 
                            }
                            //将用户账号和密码存入cookie
                            CookieOptions option = new CookieOptions(); 
                            option.Expires = DateTime.Now.AddDays(7); 
                            Response.Cookies.Append("UserName", user.UserName, option); 
                            Response.Cookies.Append("UserPwd", pwdMD5, option); //存的密码是加密过的
                            Response.Cookies.Append("remember", "1", option); 

                        }

                        return RedirectToAction("MyView","Home");
                    }
                    else
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
