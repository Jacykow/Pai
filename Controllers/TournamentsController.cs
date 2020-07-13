using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pai.Areas.Identity.Data;
using Pai.Data;
using Pai.DatabaseModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pai.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<PaiUser> _userManager;

        public TournamentsController(ApplicationDbContext context, UserManager<PaiUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (searchString == null)
            {
                searchString = "";
            }
            ViewData["CurrentFilter"] = searchString;

            var tournaments = _context.Tournament.Include("TournamentUser").Where(t => t.Title.Contains(searchString)).OrderByDescending(t => t.Time);

            return View(await PaginatedList<Tournament>.CreateAsync(tournaments.AsNoTracking(), pageNumber ?? 1, 10));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournament
                .Include(t => t.Sponsor)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

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
                    if (tournament.EntryDateLimit.HasValue == false
                        || tournament.EntryDateLimit.Value < DateTime.Now
                        || tournament.Time.HasValue == false
                        || tournament.Time.Value < tournament.EntryDateLimit.Value)
                    {
                        ModelState.AddModelError("", "The entry date limit has to be after today and before the tournament date.");
                    }
                    else if (string.IsNullOrEmpty(tournament.Title))
                    {
                        ModelState.AddModelError("", "The tournament title cannot be empty.");
                    }
                    else
                    {
                        tournament.AssignedPlayersAmount = 0;
                        _context.Add(tournament);
                        _context.Add(new TournamentUser
                        {
                            Tournament = tournament,
                            IsAdmin = true,
                            UserId = _userManager.GetUserId(User),
                            LicenceNumber = Guid.NewGuid().ToString(),
                            RankNumber = (int)DateTime.Now.Ticks
                        });
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (DbUpdateException e)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(tournament);
        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Tournament
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var tournamentToDelete = new Tournament() { Id = id };
                _context.Entry(tournamentToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        public async Task<IActionResult> Join(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Tournament
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinConfirmed(int id)
        {
            try
            {
                var tournament = _context.Tournament
                    .Where(tournament => tournament.Id == id).FirstOrDefault();
                _context.Add(new TournamentUser
                {
                    Tournament = tournament,
                    IsAdmin = false,
                    UserId = _userManager.GetUserId(User),
                    LicenceNumber = Guid.NewGuid().ToString(),
                    RankNumber = (int)DateTime.Now.Ticks
                });
                tournament.AssignedPlayersAmount++;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Join), new { id = id, saveChangesError = true });
            }
        }

        public async Task<IActionResult> Leave(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Tournament
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveConfirmed(int id)
        {
            try
            {
                var tournament = _context.Tournament
                    .Where(tournament => tournament.Id == id).FirstOrDefault();
                var tournamentUser = _context.TournamentUser
                    .Where(tournamentUser => tournamentUser.Tournament == tournament)
                    .Where(tournamentUser => tournamentUser.UserId == _userManager.GetUserId(User))
                    .FirstOrDefault();
                _context.Remove(tournamentUser);
                tournament.AssignedPlayersAmount--;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Join), new { id = id, saveChangesError = true });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var tournamentToUpdate = await _context.Tournament.FirstOrDefaultAsync(t => t.Id == id);
            if (await TryUpdateModelAsync<Tournament>(
                tournamentToUpdate,
                "",
                t => t.Title,
                t => t.Discipline,
                t => t.Location,
                t => t.EntryLimit,
                t => t.EntryDateLimit))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(tournamentToUpdate);
        }
    }
}
