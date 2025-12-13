using System.ComponentModel.DataAnnotations;

namespace BookLog.Dtos {
    public class AuthorDto {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Nationality { get; set; }

        [Range(0, 3000)]
        public int? YearOfBirth { get; set; }

        [Range(0, 9999)]
        public int? YearOfDeath { get; set; }

        [Url]
        public string? DatabazeKnihUrl { get; set; }

        public string FullName => string.Join(' ', new[] { FirstName, MiddleName, LastName }
            .Where(x => !string.IsNullOrWhiteSpace(x)));

        public string NationalityDisplay => string.IsNullOrWhiteSpace(Nationality) ? "unknown" : Nationality;

        public string? LifeSpan => YearOfBirth.HasValue
            ? YearOfDeath.HasValue
                ? $"{YearOfBirth}-{YearOfDeath}"
                : YearOfBirth.ToString()
            : string.Empty;
    }
}
