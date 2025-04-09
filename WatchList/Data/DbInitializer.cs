using WatchList.Models;

namespace WatchList.Data
{
    public class DbInitializer
    {
        private readonly WatchListContext _context;

        public DbInitializer(WatchListContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();

            if (_context.WatchItems.Any())
            {
                return; 
            }

            var initialItems = new WatchItem[]
            {
                new WatchItem { Title = "Крестный отец", Type = "Movie", Status = "Watched" },
                new WatchItem { Title = "Игра престолов", Type = "Series", Status = "Watching" },
                new WatchItem { Title = "Форрест Гамп", Type = "Movie", Status = "ToWatch" }
            };

            _context.WatchItems.AddRange(initialItems);
            _context.SaveChanges();
        }
    }
}