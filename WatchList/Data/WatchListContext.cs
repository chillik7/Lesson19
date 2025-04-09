using Microsoft.EntityFrameworkCore;
using WatchList.Models;

namespace WatchList.Data
{
    public class WatchListContext : DbContext
    {
        public WatchListContext(DbContextOptions<WatchListContext> options)
            : base(options)
        {
        }

        public DbSet<WatchItem> WatchItems { get; set; }
    }
}