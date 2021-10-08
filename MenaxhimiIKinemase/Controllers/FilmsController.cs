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
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FilmsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Filmi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Film.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            ViewData["GetMovies"] = search;

            var film = from x in _context.Film select x;

            if (!String.IsNullOrEmpty(search))
            {
                film = film.Where(x => x.Zhanri.Contains(search) || x.Titulli.Contains(search));
            }
            return View(await film.AsNoTracking().ToListAsync());
        }

        // GET: Filmi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmi = await _context.Film
                .FirstOrDefaultAsync(m => m.FilmiID == id);
            if (filmi == null)
            {
                return NotFound();
            }

            return View(filmi);
        }

        // GET: Filmi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmiID,Titulli,Premiera,Regjisori,Zhanri,Pershkrimi,MovieFile,TrailerFile")] Film filmi)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(filmi.MovieFile.FileName);
                string extension = Path.GetExtension(filmi.MovieFile.FileName);

                filmi.MoviePicture = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/img/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await filmi.MovieFile.CopyToAsync(fileStream);
                }

                string wwwRootPath1 = _hostEnvironment.WebRootPath;
                string fileName1 = Path.GetFileNameWithoutExtension(filmi.TrailerFile.FileName);
                string extension1 = Path.GetExtension(filmi.TrailerFile.FileName);

                filmi.MovieTrailer = fileName1 = fileName1 + DateTime.Now.ToString("yymmssfff") + extension;
                string path1 = Path.Combine(wwwRootPath1 + "/videos/", fileName1);
                using (var fileStream1 = new FileStream(path1, FileMode.Create))
                {
                    await filmi.TrailerFile.CopyToAsync(fileStream1);
                }

                _context.Add(filmi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filmi);
        }

        // GET: Filmi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmi = await _context.Film.FindAsync(id);
            if (filmi == null)
            {
                return NotFound();
            }
            return View(filmi);
        }

        // POST: Filmi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmiID,Titulli,Premiera,Regjisori,Zhanri,MoviePicture")] Film filmi)
        {
            if (id != filmi.FilmiID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmiExists(filmi.FilmiID))
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
            return View(filmi);
        }

        // GET: Filmi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmi = await _context.Film
                .FirstOrDefaultAsync(m => m.FilmiID == id);
            if (filmi == null)
            {
                return NotFound();
            }

            return View(filmi);
        }

        // POST: Filmi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmi = await _context.Film.FindAsync(id);

            //delete image from wwwroot/image
            var picturePath = Path.Combine(_hostEnvironment.WebRootPath, "img", filmi.MoviePicture);
            if (System.IO.File.Exists(picturePath))
            {
                System.IO.File.Delete(picturePath);
            }

            _context.Film.Remove(filmi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmiExists(int id)
        {
            return _context.Film.Any(e => e.FilmiID == id);
        }
        private string UploadedFile(Film model)
        {
            throw new NotImplementedException();
        }
    }
}
