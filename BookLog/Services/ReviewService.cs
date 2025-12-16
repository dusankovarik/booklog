using BookLog.Dtos;
using BookLog.Models;
using BookLog.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookLog.Services {
    public class ReviewService {
        ApplicationDbContext _dbContext;

        public ReviewService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<BookDetailsDto?> GetBookDetailsAsync(int bookId) {
            var book = await _dbContext.Books.FindAsync(bookId);
            if (book == null) {
                return null;
            }
            var bookDetailsDto = new BookDetailsDto() {
                Id = book.Id,
                Title = book.Title,
                CoverImageUrl = book.CoverImageUrl,
                DatabazeKnihUrl = book.DatabazeKnihUrl,
            };
            bookDetailsDto.Authors = book.Authors
                .Select(a => string.Join(' ', new[] { a.FirstName, a.MiddleName, a.LastName }
                .Where(x => !string.IsNullOrWhiteSpace(x)))).ToList();
            bookDetailsDto.Genres = book.Genres.OrderBy(g => g.Name).Select(g => g.Name).ToList();
            return bookDetailsDto;
        }

        public async Task<IReadOnlyList<ReviewDto>> GetByBookIdAsync(int bookId) {
            var reviews = await _dbContext.Reviews.Where(r => r.BookId == bookId).OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            var reviewDtos = new List<ReviewDto>();
            foreach (var review in reviews) {
                reviewDtos.Add(ModelToDto(review));
            }
            return reviewDtos;
        }

        private ReviewDto ModelToDto(Review review) {
            return new ReviewDto() {
                Id = review.Id,
                Rating = review.Rating,
                Text = review.Text,
                CreatedAt = review.CreatedAt,
            };
        }
    }
}
