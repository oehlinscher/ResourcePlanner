using Microsoft.EntityFrameworkCore;
using ResourcePlanner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePlanner.Core
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Resource> Resources { get; set; }

        // You can override OnModelCreating if you need custom configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.Id);  // Primary key

                entity.Property(e => e.Name)
                    .IsRequired()  // Required field
                    .HasMaxLength(100);  // Max length of 100 characters
            });

        }
    }
}
