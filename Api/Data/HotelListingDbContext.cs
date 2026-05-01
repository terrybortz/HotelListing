using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : DbContext(options)
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<Hotel> Hotels { get; set; }

}
