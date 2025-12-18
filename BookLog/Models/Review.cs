namespace BookLog.Models {
    public class Review {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public int Rating { get; set; }
        public required string Text { get; set; }
        public DateOnly CreatedAt { get; set; }
        public required string UserName { get; set; }
    }
}
