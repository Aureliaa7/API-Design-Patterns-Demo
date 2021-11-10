using System;
using System.ComponentModel.DataAnnotations;

namespace API_DesignPatterns.Core.DTOs
{
    public class AddBookDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int PublishingYear { get; set; }

        [Required]
        public string PublishingHouse { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}
