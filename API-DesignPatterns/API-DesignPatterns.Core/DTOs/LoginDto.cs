using System.ComponentModel.DataAnnotations;

namespace API_DesignPatterns.Core.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
