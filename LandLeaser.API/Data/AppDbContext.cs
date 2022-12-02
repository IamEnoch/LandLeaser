using LandLeaser.API.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LandLeaser.API.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingImage> Images { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
