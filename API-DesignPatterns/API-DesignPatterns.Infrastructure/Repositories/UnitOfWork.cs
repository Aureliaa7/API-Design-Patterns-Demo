using API_DesignPatterns.Core.DomainEntities;
using API_DesignPatterns.Core.Interfaces.Repositories;
using API_DesignPatterns.Infrastructure.DatabaseContext;
using System.Threading.Tasks;

namespace API_DesignPatterns.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private IRepository<Book> bookRepository;
        private IRepository<Author> authorRepository;
        private IRepository<AuthorBook> authorBookRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<Book> BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new Repository<Book>(dbContext);
                }
                return bookRepository;
            }
        }

        public IRepository<Author> AuthorRepository
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new Repository<Author>(dbContext);
                }
                return authorRepository;
            }
        }

        public IRepository<AuthorBook> AuthorBookRepository
        {
            get
            {
                if (authorBookRepository == null)
                {
                    authorBookRepository = new Repository<AuthorBook>(dbContext);
                }
                return authorBookRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
