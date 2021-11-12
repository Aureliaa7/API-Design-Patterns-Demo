using Microsoft.AspNetCore.Identity;
using System;

namespace API_DesignPatterns.Infrastructure.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
