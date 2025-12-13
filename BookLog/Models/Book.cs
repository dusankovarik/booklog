namespace BookLog.Models {
    public class Book {
        public int Id { get; set; }
        public required string Title { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public string? CoverImageUrl { get; set; }
        public string? DatabazeKnihUrl { get; set; }
    }
}
