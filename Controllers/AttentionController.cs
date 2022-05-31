using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class AttentionController : Controller
    {
        private readonly MvcAttentionContext _context;


        public AttentionController(MvcAttentionContext context)
        {
            _context = context;
        }

        // GET: Attention
        public async Task<IActionResult> Index()
        {
              return View(await _context.Attention.ToListAsync());
        }

        // GET: Attention/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attention == null)
            {
                return NotFound();
            }

            var attention = await _context.Attention
                .FirstOrDefaultAsync(m => m.AttentionID == id);
            if (attention == null)
            {
                return NotFound();
            }

            return View(attention);
        }

        // GET: Attention/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attention/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,AttentionID")] Attention attention)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attention);
        }

        // GET: Attention/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attention == null)
            {
                return NotFound();
            }

            var attention = await _context.Attention.FindAsync(id);
            if (attention == null)
            {
                return NotFound();
            }
            return View(attention);
        }

        // POST: Attention/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,AttentionID")] Attention attention)
        {
            if (id != attention.AttentionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttentionExists(attention.AttentionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(attention);
        }

        // GET: Attention/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attention == null)
            {
                return NotFound();
            }

            var attention = await _context.Attention
                .FirstOrDefaultAsync(m => m.AttentionID == id);
            if (attention == null)
            {
                return NotFound();
            }

            return View(attention);
        }

        // POST: Attention/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attention == null)
            {
                return Problem("Entity set 'MvcAttentionContext.Attention'  is null.");
            }
            var attention = await _context.Attention.FindAsync(id);
            if (attention != null)
            {
                _context.Attention.Remove(attention);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttentionExists(int id)
        {
          return _context.Attention.Any(e => e.AttentionID == id);
        }


        // GET: Attention/IsAttention 判断某用户是否已关注
        public ActionResult IsAttention(int otherid){
            int userid;
            int.TryParse(HttpContext.Session.GetString("user"),out userid);
            bool isAttention = _context.Attention.Any(a => a.AttentionID == otherid && a.UserID == userid );
            if(isAttention){
                ViewData["attention"] = "取消关注";
            }else{
                ViewData["attention"] = "关注";
            }
            return View("/Views/Attention/Index.cshtml");
        }

/*         public IActionResult AttentionClick(){

            return View("/Views/Attention/Index.cshtml");

        } */



        // POST: Attention/AttentionClick 加入关注
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttentionClick(int attentionid)
        {
            Attention attention = new Attention();
            if (ModelState.IsValid)
            {
                int id;
                if(int.TryParse(HttpContext.Session.GetString("user"),out id))
                {
                    attention.UserID = id;
                    attention.AttentionID = attentionid;
                    _context.Add(attention);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("/Views/Attention/Index.cshtml");
        }
        // GET: Attention/FindAttention
        public async Task<IActionResult> FindAttention()
        {
            int id;
            int.TryParse(HttpContext.Session.GetString("user"),out id);
            //var userinfo = await _context.UserInfo.FromSqlRaw(string.Format("select * from UserInfo join Attention where Attention.UserID='{0}' and UserInfo.ID = Attention.AttentionID", id)).ToListAsync();


            var user = from _Attention in _context.Attention
            from _UserInfo in _context.UserInfo
            where  _Attention.UserID == id && _UserInfo.UserID == _Attention.AttentionID
            select _UserInfo;   

            return View("/Views/UserInfo/Index.cshtml",await user.ToListAsync());
      
        }

    
    }
}
