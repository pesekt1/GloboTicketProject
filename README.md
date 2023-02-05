#GloboTicket

not idempotent POST venue

simulate an error in the response to simulate the problem with idempotence of POST request:
VenuesController:
```
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueId,Name,City")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                await Task.Delay(3000);
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }
```
Now in the venues form, fill some name and city and press the Create button multiple times. 
You will get multiple identical records in the database - with different IDs.