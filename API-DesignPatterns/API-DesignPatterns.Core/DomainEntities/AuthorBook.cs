using System;

namespace API_DesignPatterns.Core.DomainEntities
{
    public class AuthorBook
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public Guid AuthorId { get; set; }

        public bool IsDeleted { get; set; }


        // navigation properties
        public Book Book { get; set; }

        public Author Author { get; set; }
    }
}
