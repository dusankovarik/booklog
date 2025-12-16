using System.ComponentModel.DataAnnotations;

namespace BookLog.Dtos {
    public class ReviewCreateDto {
        public int BookId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;
    }
}
