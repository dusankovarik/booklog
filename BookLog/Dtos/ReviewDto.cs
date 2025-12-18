namespace BookLog.Dtos {
    public class ReviewDto {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
