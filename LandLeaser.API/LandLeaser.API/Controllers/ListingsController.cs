using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LandLeaser.API.Data;
using LandLeaser.API.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace LandLeaser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ListingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Listings
        [HttpGet]
        public async Task<ActionResult<IList<Listing>>> GetListings()
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }

            var query = _context.Listings
                .Join(_context.Images, c => c.Id, v => v.ListingId, (c, v) => new { c, v });

            var listings = new List<Listing>();
            foreach (var VARIABLE in query)
            {
                var listing = new Listing()
                {
                    Id = VARIABLE.c.Id,
                    Location = VARIABLE.c.Location,
                    Size = VARIABLE.c.Size,
                    Cost = VARIABLE.c.Cost,
                    Duration = VARIABLE.c.Duration,
                    Description = VARIABLE.c.Description,
                    AppUserId = VARIABLE.c.AppUserId,
                    Images = VARIABLE.c.Images,
                    ApplicationUser = VARIABLE.c.ApplicationUser
                };
                listings.Add(listing);

            }

            return listings;
            

        }


        // GET: api/Listings/93345CA0-30F1-48C1-8B42-DC7C8DE88B60
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(Guid id)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings.FindAsync(id);

            if (listing == null)
            {
                return NotFound();
            }

            var query = _context.Images.Where(c => c.ListingId == id);
            foreach (var listingImage in query)
            {
                listing.Images.Add(listingImage);
            }


            return Ok(listing);
        }

        // PUT: api/Listings/14ab1492-f6f0-434a-8090-9e81ad1d2c99
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(Guid id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            _context.Entry(listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Listings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing(Listing listing)
        {
            if (_context.Listings == null)
            {
                return Problem("Entity set 'AppDbContext.Listings'  is null.");
            } 
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListing", new { id = listing.Id }, listing);
        }

        // DELETE: api/Listings/14ab1492-f6f0-434a-8090-9e81ad1d2c99
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(Guid id)
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListingExists(Guid id)
        {
            return (_context.Listings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
