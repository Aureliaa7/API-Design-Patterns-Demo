using API_DesignPatterns.Core.DomainEntities;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Book> BookRepository { get; }

        IRepository<Author> AuthorRepository { get; }
        
        IRepository<AuthorBook> AuthorBookRepository { get; }
        
        Task SaveChangesAsync();
    }
}
