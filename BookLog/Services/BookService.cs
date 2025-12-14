using BookLog.Models;

namespace BookLog.Services {
    public class BookService {
        ApplicationDbContext _dbContext;

        public BookService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
    }
}
