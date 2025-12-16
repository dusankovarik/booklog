namespace BookLog.Dtos {
    public class BookDetailsDto {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public IReadOnlyList<string> Authors { get; set; } = [];
        public IReadOnlyList<string> Genres { get; set; } = [];
        public string? CoverImageUrl { get; set; }
        public string? DatabazeKnihUrl { get; set; }
    }
}
