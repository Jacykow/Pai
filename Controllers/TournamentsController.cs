using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pai.Data;
using Pai.Models;
using System.Threading.Tasks;

namespace Pai.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly AppDbContext _context;

        public TournamentsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tournaments.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .Include(t => t.Sponsors)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.ID == id);

            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Discipline,Time,Location,EntryLimit,EntryDateLimit,Sponsors")] Tournament tournament)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tournament.AssignedPlayersAmount = 0;
                    _context.Add(tournament);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(tournament);
        }
    }
}
