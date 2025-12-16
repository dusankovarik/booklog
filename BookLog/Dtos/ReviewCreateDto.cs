using System.ComponentModel.DataAnnotations;

namespace BookLog.Dtos {
    public class ReviewCreateDto {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please select a rating.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        [Required(ErrorMessage = "Review text is required.")]
        [StringLength(2000, ErrorMessage = "Review is too long.")]
        public string Text { get; set; } = string.Empty;
    }
}
