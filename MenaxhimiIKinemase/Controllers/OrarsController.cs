using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenaxhimiIKinemase.Data;
using MenaxhimiIKinemase.Models;

namespace MenaxhimiIKinemase.Controllers
{
    public class OrarsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public OrarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orari
        public async Task<IActionResult> Index()
        {
            var kinemaDbContext = _context.Orar.Include(o => o.Filmi);
            return View(await kinemaDbContext.ToListAsync());
        }

        // GET: Orari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orari = await _context.Orar
                .Include(o => o.Filmi)
                .FirstOrDefaultAsync(m => m.OrariID == id);
            if (orari == null)
            {
                return NotFound();
            }

            return View(orari);
        }

        // GET: Orari/Create
        public IActionResult Create()
        {
            ViewData["FilmiID"] = new SelectList(_context.Film, "FilmiID", "Titulli");
            return View();
        }

        // POST: Orari/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrariID,Ora,FilmiID")] Orar orari)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orari);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmiID"] = new SelectList(_context.Film, "FilmiID", "Titulli", orari.FilmiID);
            return View(orari);
        }

        // GET: Orari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orari = await _context.Orar.FindAsync(id);
            if (orari == null)
            {
                return NotFound();
            }
            ViewData["FilmiID"] = new SelectList(_context.Film, "FilmiID", "Titulli", orari.FilmiID);
            return View(orari);
        }

        // POST: Orari/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrariID,Ora,FilmiID")] Orar orari)
        {
            if (id != orari.OrariID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orari);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrariExists(orari.OrariID))
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
            ViewData["FilmiID"] = new SelectList(_context.Film, "FilmiID", "Titulli", orari.FilmiID);
            return View(orari);
        }

        // GET: Orari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orari = await _context.Orar
                .Include(o => o.Filmi)
                .FirstOrDefaultAsync(m => m.OrariID == id);
            if (orari == null)
            {
                return NotFound();
            }

            return View(orari);
        }

        // POST: Orari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orari = await _context.Orar.FindAsync(id);
            _context.Orar.Remove(orari);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrariExists(int id)
        {
            return _context.Orar.Any(e => e.OrariID == id);
        }
    }
}
