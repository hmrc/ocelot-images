using ImageAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAPI.Contexts
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        { }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Image>(entity =>
            {
                entity.HasIndex(e => e.Placeholder).IsUnique();
            });

            // builder.Entity<Image>()
            // .HasAlternateKey(c => c.Placeholder);
        }

    }
}
