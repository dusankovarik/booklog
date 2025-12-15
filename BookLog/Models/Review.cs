namespace BookLog.Models {
    public class Review {
        public int Id { get; set; }
        public int BookId { get; set; }
        public required Book Book { get; set; }
        public int Rating { get; set; }
        public required string Text { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
