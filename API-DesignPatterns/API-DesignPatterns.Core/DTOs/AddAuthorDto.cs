using System.ComponentModel.DataAnnotations;

namespace API_DesignPatterns.Core.DTOs
{
    public class AddAuthorDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        // field needed to implement request validation
        public bool ValidateOnly { get; set; } = false;
    }
}
