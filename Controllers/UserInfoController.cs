using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using System.Web;
using System.IO;


namespace MvcMovie.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly MvcAttentionContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserInfoController(MvcAttentionContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: UserInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserInfo.ToListAsync());
        }

        /*         // GET: UserInfo/Details/5
                public async Task<IActionResult> Details(int? id)
                {
                    if (id == null || _context.UserInfo == null)
                    {
                        return NotFound();
                    }

                    var userInfo = await _context.UserInfo
                        .FirstOrDefaultAsync(m => m.UserID == id);
                    if (userInfo == null)
                    {
                        return NotFound();
                    }

                    // return RedirectToAction("MyView","Home",userInfo);

                    return View("/Views/UserInfo/MyView.cshtml",userInfo);
                } */


        // GET: UserInfo/Details/5
        public async Task<UserInfo> Details1(int? id)
        {
            if (id == null || _context.UserInfo == null)
            {
                return null;
            }
            var userInfo = await _context.UserInfo
                .FirstOrDefaultAsync(m => m.UserID == id);

            return userInfo;
        }

        // GET: UserInfo/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult UserInfo()
        {
            return View();
        }

        // POST: UserInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName,Email,Avatar,Signature,Label1")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                int id;
                int.TryParse(HttpContext.Session.GetString("user"), out id);
/*                 if (userInfo.UserName == null)
                {
                    userInfo.UserID = id;
                    userInfo.UserName = HttpContext.Session.GetString("userMame");
                } */
   
                _context.Add(userInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }

        // GET: UserInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             if (id == null || _context.UserInfo == null)
            {
                return RedirectToAction(nameof(Create));
            }

            var userInfo = await _context.UserInfo
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userInfo == null)
            {
                return RedirectToAction(nameof(Create));
            }
            return View("/Views/Home/UserInfo.cshtml", userInfo);
        }

        // POST: UserInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,UserName,Email,Avatar,Signature,Label1,Label2,Label3")] UserInfo userInfo)
        {
            if (id != userInfo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInfoExists(userInfo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("/Views/Home/UserInfo.cshtml", userInfo);
            }
            return View("/Views/Home/UserInfo.cshtml", userInfo);
        }
        public async Task<IActionResult> Edit1(int? id, UserInfo userInfo)
        {
            if (id != userInfo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInfoExists(userInfo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("/Views/Home/UserInfo.cshtml",userInfo);
            }
            return View("/Views/Home/UserInfo.cshtml",userInfo);
        }
        // GET: UserInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserInfo == null)
            {
                return NotFound();
            }

            var userInfo = await _context.UserInfo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }

        // POST: UserInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserInfo == null)
            {
                return Problem("Entity set 'MvcAttentionContext.UserInfo'  is null.");
            }
            var userInfo = await _context.UserInfo.FindAsync(id);
            if (userInfo != null)
            {
                _context.UserInfo.Remove(userInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInfoExists(int id)
        {
            return _context.UserInfo.Any(e => e.ID == id);
        }


        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            int userid;
            int.TryParse(HttpContext.Session.GetString("user"), out userid);
            try
            {
                if (file.Length > 0)
                {

                    string fileExt = Path.GetExtension(file.FileName); //获得扩展名
                    //long fileSize = file.Length; //获得文件大小，以字节为单位
                    string newFileName = System.Guid.NewGuid().ToString() + fileExt; //随机生成新的文件名
                    var filePath = webRootPath + "/upload/";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    var filepath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(Path.Combine(filePath, newFileName)))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var userInfo = await _context.UserInfo.FirstOrDefaultAsync(m => m.UserID == userid);
                    var realpath = "/upload/" + newFileName;
                    userInfo.Avatar = realpath;
                    _context.Update(userInfo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Edit", routeValues: new { id = userid });


                }
            }
            catch
            {

            }
            return RedirectToAction("Edit", routeValues: new { id = userid });

        }


    }
}