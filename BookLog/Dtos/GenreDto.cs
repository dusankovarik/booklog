using System.ComponentModel.DataAnnotations;

namespace BookLog.Dtos {
    public class GenreDto {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
