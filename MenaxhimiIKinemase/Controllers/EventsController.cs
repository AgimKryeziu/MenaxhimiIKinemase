using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenaxhimiIKinemase.Data;
using MenaxhimiIKinemase.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MenaxhimiIKinemase.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EventsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        // GET: Eventis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Event.ToListAsync());
        }

        // GET: Eventis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventi = await _context.Event
                .FirstOrDefaultAsync(m => m.EventiID == id);
            if (eventi == null)
            {
                return NotFound();
            }

            return View(eventi);
        }

        // GET: Eventis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventiID,Emertimi,Pershkrimi,Data,EventFile")] Event eventi)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(eventi.EventFile.FileName);
                string extension = Path.GetExtension(eventi.EventFile.FileName);

                eventi.EventPicture = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/img/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await eventi.EventFile.CopyToAsync(fileStream);
                }

                _context.Add(eventi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(eventi);
        }

        // GET: Eventis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventi = await _context.Event.FindAsync(id);
            if (eventi == null)
            {
                return NotFound();
            }
            return View(eventi);
        }

        // POST: Eventis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventiID,Emertimi,Pershkrimi,Data,EventFile")] Event eventi)
        {
            if (id != eventi.EventiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventiExists(eventi.EventiID))
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
            return View(eventi);
        }

        // GET: Eventis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventi = await _context.Event
                .FirstOrDefaultAsync(m => m.EventiID == id);
            if (eventi == null)
            {
                return NotFound();
            }

            return View(eventi);
        }

        // POST: Eventis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventi = await _context.Event.FindAsync(id);
            _context.Event.Remove(eventi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventiExists(int id)
        {
            return _context.Event.Any(e => e.EventiID == id);
        }
    }
}
