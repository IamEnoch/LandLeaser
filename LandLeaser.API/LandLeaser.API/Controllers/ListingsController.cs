using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LandLeaser.API.Data;
using LandLeaser.API.Data.Models;

namespace LandLeaser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ListingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Listings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            return await _context.Listings.ToListAsync();

        }

        // GET: api/Images
        [HttpGet("api/Images")]
        public async Task<ActionResult<IEnumerable<ListingImage>>> GetImages()
        {
            if (_context.Images == null)
            {
                return NotFound();
            }
            return await _context.Images.ToListAsync();

        }

        // GET: api/Listings/5
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

            return listing;
        }

        // PUT: api/Listings/5
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

        // DELETE: api/Listings/5
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
