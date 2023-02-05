using GloboTicket.Promotion.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Promotion.Venues
{
    public class VenuesController : Controller
    {
        private readonly VenueQueries queries;
        private readonly VenueCommands commands;

        public VenuesController(VenueQueries queries, VenueCommands commands)
        {
            this.queries = queries;
            this.commands = commands;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            return View(await queries.ListVenues());
        }

        // GET: Venues/Details/abc-123
        public async Task<IActionResult> Details(Guid id)
        {
            var venue = await queries.GetVenue(id);
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
        public async Task<IActionResult> Create(Guid id, [Bind("VenueId,Name,City")] VenueInfo venue)
        {
            if (ModelState.IsValid)
            {
                venue.VenueGuid = id;
                await commands.SaveVenue(venue);
                await Task.Delay(3000);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/abc-123
        public async Task<IActionResult> Edit(Guid id)
        {
            var venue = await queries.GetVenue(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("VenueId,Name,City")] VenueInfo venue)
        {
            if (ModelState.IsValid)
            {
                venue.VenueGuid = id;
                await commands.SaveVenue(venue);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Delete/abc-123
        public async Task<IActionResult> Delete(Guid id)
        {
            var venue = await queries.GetVenue(id);
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
            await commands.DeleteVenue(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
