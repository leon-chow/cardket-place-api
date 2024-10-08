using cardket_place_api.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cardket_place_api
{
    public class DataContext : IdentityDbContext<Account>
    {
        protected readonly IConfiguration configuration;

        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Card> Cards { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set;}
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
    }
}