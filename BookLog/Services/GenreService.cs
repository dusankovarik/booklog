using BookLog.Models;

namespace BookLog.Services {
    public class GenreService {
        private ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context) {
            _context = context;
        }
    }
}
