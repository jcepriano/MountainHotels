using Microsoft.EntityFrameworkCore;
using MountainHotels.Models;

namespace MountainHotels.DataAccess
{
    public class MountainHotelsContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public MountainHotelsContext(DbContextOptions<MountainHotelsContext> options)
            : base(options) { }
    }
}
