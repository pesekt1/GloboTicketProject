using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GloboTicket.Promotion.Data;
using GloboTicket.Promotion.Venues;

namespace GloboTicket.Promotion.Controllers
{
    public class VenuesController : Controller
    {
        private readonly PromotionContext _context;

        public VenuesController(PromotionContext context)
        {
            _context = context;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venue.ToListAsync());
        }

        // GET: Venues/Details/abc-123
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue
                .SingleOrDefaultAsync(m => m.VenueGuid == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venues/Create
        // GET: Venues/Create/abc-123
        public IActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Create), new { id = Guid.NewGuid() });
            }
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, [Bind("VenueId,Name,City")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                venue.VenueGuid = id;
                var venueId = await _context.Venue
                    .Where(m => m.VenueGuid == id)
                    .Select(m => m.VenueId)
                    .SingleOrDefaultAsync();
                if (venueId == 0)
                {
                    _context.Add(venue);
                }
                else
                {
                    venue.VenueId = venueId;
                    _context.Update(venue);
                }

                await _context.SaveChangesAsync();
                await Task.Delay(3000);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/abc-123
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue.
                SingleOrDefaultAsync(m => m.VenueGuid == id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/abc-123
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VenueId,Name,City")] Venue venue)
        {
            venue.VenueGuid = id;

            if (ModelState.IsValid)
            {
                var venueId = await _context.Venue
                    .Where(m => m.VenueGuid == id)
                    .Select(m => m.VenueId)
                    .SingleOrDefaultAsync();
                if (venueId == 0)
                {
                    return NotFound();
                }
                else
                {
                    venue.VenueId = venueId;
                    _context.Update(venue);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Delete/abc-123
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue
                .SingleOrDefaultAsync(m => m.VenueGuid == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/abc-123
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var venue = await _context.Venue
                .SingleOrDefaultAsync(m => m.VenueGuid == id);
            _context.Venue.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.VenueId == id);
        }
    }
}
