using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure the 'Stock' entity is mapped to the correct table name in the database
            modelBuilder.Entity<Stock>()
                .ToTable("Stocks"); // Ensure this matches your actual table name in the database

            // If the 'Comments' table is also problematic, you can do the same:
            modelBuilder.Entity<Comment>()
                .ToTable("Comments"); // Optional: explicitly map if needed
        }
    }

    
}
