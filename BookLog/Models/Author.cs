namespace BookLog.Models {
    public class Author {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public string? Nationality { get; set; }
        public int YearOfBirth { get; set; }
        public int? YearOfDeath { get; set; }
        public string? DatabazeKnihUrl { get; set; }
    }
}
