using System;

namespace API_DesignPatterns.Core.DTOs
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
