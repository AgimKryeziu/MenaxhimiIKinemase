using MenaxhimiIKinemase.Data;
using MenaxhimiIKinemase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MenaxhimiIKinemase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public async Task<IActionResult> IndexDetails(int? id)
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
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> IndexFilm()
        {
            var film = await _context.Film.ToListAsync();
            return View(film);
        }

        public async Task<IActionResult> Evente()
        {
            var film = await _context.Event.ToListAsync();
            return View(film);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
    }
}
