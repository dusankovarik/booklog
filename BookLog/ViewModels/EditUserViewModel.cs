using System.ComponentModel.DataAnnotations;

namespace BookLog.ViewModels {
    public class EditUserViewModel {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
