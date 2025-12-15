using System.ComponentModel.DataAnnotations;

namespace BookLog.Dtos {
    public class BookCreateEditDto {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [MinLength(1, ErrorMessage = "Select at least one author")]
        public List<int> SelectedAuthorIds { get; set; } = [];

        [MinLength(1, ErrorMessage = "Select at least one genre")]
        public List<int> SelectedGenreIds { get; set; } = [];

        public IReadOnlyList<AuthorDto> Authors { get; set; } = [];
        public IReadOnlyList<GenreDto> Genres { get; set; } = [];

        public string? CoverImageUrl { get; set; }
        public string? DatabazeKnihUrl { get; set; }
    }
}
