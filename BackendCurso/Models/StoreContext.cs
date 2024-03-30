﻿using Microsoft.EntityFrameworkCore;

namespace BackendCurso.Models
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Beer> Beers { get; set; }
    }
}
