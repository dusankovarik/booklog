using BookLog.Dtos;

namespace BookLog.ViewModels {
    public class BookDetailsViewModel {
        public BookDetailsDto Book { get; set; } = null!;
        public IReadOnlyList<ReviewDto> Reviews { get; set; } = [];
        public ReviewCreateDto NewReview { get; set; } = new();
    }
}
