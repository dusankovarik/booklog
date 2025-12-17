using System.ComponentModel.DataAnnotations;

namespace BookLog.ViewModels {
    public class LoginViewModel {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }

        public bool Remember { get; set; }
    }
}
