using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LandLeaser.API.Data;
using LandLeaser.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using LandLeaser.API.Models;

namespace LandLeaser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ListingsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Listings
        [HttpGet]
        [AllowAnonymous]
        private async Task<ActionResult<IEnumerable<GetListingDto>>> GetListings()
        {
            if (_context.Listings == null)
            {
                return NotFound();
            }
            

            var listings = _context.Listings.GroupJoin(_context.Images,
                x => x.Id, y => y.ListingId,
                (listing, image) => new
                {
                    listing,
                    image
                }).SelectMany(x => x.image.DefaultIfEmpty(), (x, y) =>
                new GetListingDto()
                {
                    AppUserId = x.listing.AppUserId,
                    Cost = x.listing.Cost,
                    Description = x.listing.Description,
                    Duration = x.listing.Duration,
                    Id = x.listing.Id.ToString(),
                    Images = _mapper.Map<IList<ListingImageDto>>(x.image),
                    Location = x.listing.Location,
                    Size = x.listing.Size
                });
            return Ok(listings);
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
        public async Task<ActionResult<Listing>> PostListing([FromBody]CreateListingDto createListing)
        {
            if (_context.Listings == null)
            {
                return Problem("Entity set 'AppDbContext.Listings'  is null.");
            }

            var listing = new Listing(createListing.Location, createListing.Size, createListing.Cost,
                createListing.Duration, createListing.Description, createListing.AppUserId)
            {
                Images = _mapper.Map<ICollection<ListingImage>>(createListing.Images)
            };
            
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            var responseListing = _mapper.Map<GetListingDto>(listing);

            return CreatedAtAction("GetListing", new { id = responseListing.Id }, responseListing);
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
