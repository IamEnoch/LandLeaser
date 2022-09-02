﻿using Microsoft.EntityFrameworkCore;

namespace LandLeaser.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {                
        }
    }
}
