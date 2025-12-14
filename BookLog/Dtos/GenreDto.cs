using System.ComponentModel.DataAnnotations;

namespace BookLog.Dtos {
    public class GenreDto {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
