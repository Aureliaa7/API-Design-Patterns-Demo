using API_DesignPatterns.Core.DomainEntities;
using API_DesignPatterns.Core.Exceptions;
using API_DesignPatterns.Core.Interfaces.DomainServices;
using API_DesignPatterns.Core.Interfaces.Repositories;
using API_DesignPatterns.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.DomainServices
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Book> AddAsync(AddBookModel book)
        {
            var addedBoook = await unitOfWork.BookRepository.AddAsync(new Book
            {
                Title = book.Title,
                Description = book.Description,
                PublishingHouse = book.PublishingHouse,
                PublishingYear = book.PublishingYear,
                Id = Guid.NewGuid()
            });

            var authorBooks = new List<AuthorBook>();
            foreach (var authorId in book.AuthorIds)
            {
                bool authorExists = await unitOfWork.AuthorRepository.ExistsAsync(
                    a => a.Id == authorId && !a.IsDeleted);
                if (!authorExists)
                {
                    throw new EntityNotFoundException($"The book could not be saved since the author with the id {authorId}" +
                        " does not exist!");
                }

                authorBooks.Add(new AuthorBook{
                    AuthorId = authorId,
                    BookId = addedBoook.Id
                });
            }

            await unitOfWork.AuthorBookRepository.AddRangeAsync(authorBooks);

            if (!book.ValidateOnly)
            {
                await unitOfWork.SaveChangesAsync();
            }

            return addedBoook;
        }

        public async Task DeleteAsync(Guid id, bool validateOnly = false)
        {
            await unitOfWork.BookRepository.RemoveAsync(id);
            if (!validateOnly)
            {
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookModel>> GetAllAsync()
        {
            var books = (await unitOfWork.BookRepository.GetAllAsync(b => !b.IsDeleted)).ToList();
            var bookModels = new List<BookModel>();

            books.ForEach(async book => {
                bookModels.Add(await GetBookModelAsync(book));
            });

            return bookModels;
        }

        private async Task<BookModel> GetBookModelAsync(Book book)
        {
            var authorsIds = (await unitOfWork.AuthorBookRepository.GetAllAsync(ab => ab.BookId == book.Id))
                .Select(ab => ab.AuthorId).ToList();
            var authorsNames = (await unitOfWork.AuthorRepository.GetAllAsync(a => authorsIds.Contains(a.Id)))
            .Select(x => $"{x.FirstName} {x.LastName}").ToList();

            return new BookModel
            {
                Id = book.Id,
                IsDeleted = book.IsDeleted,
                Description = book.Description,
                Title = book.Title,
                PublishingYear = book.PublishingYear,
                PublishingHouse = book.PublishingHouse,
                AuthorNames = string.Join(", ", authorsNames)
            };
        }

        public async Task<BookModel> GetByIdAsync(Guid id)
        {
            var book = await unitOfWork.BookRepository.GetFirstOrDefaultAsync(b => b.Id == id);

            return await GetBookModelAsync(book);
        }

        public async Task MarkAsDeletedAsync(Guid id, bool validateOnly = false)
        {
            await ChangeIsDeletedStatusAsync(id, true);

            if (!validateOnly)
            {
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<BookModel> RestoreAsync(Guid id, bool validateOnly = false)
        {
            var book = await ChangeIsDeletedStatusAsync(id, false);
            
            if (!validateOnly)
            {
                await unitOfWork.SaveChangesAsync();
            }

            return await GetBookModelAsync(book);
        }

        private async Task<Book> ChangeIsDeletedStatusAsync(Guid id, bool isDeleted)
        {
            var book = await unitOfWork.BookRepository.GetByIdAsync(id);
            book.IsDeleted = isDeleted;
            await unitOfWork.BookRepository.UpdateAsync(book);

            // When we delete a book, also the entities referencing it should be deleted (because there is a 
            // OnDeleteCascade constraint on the FK BookId from AuthorBooks table), so in this case
            // all the entities from AuthorBooks table should be marked as deleted.
            var authorBooks = (await unitOfWork.AuthorBookRepository
                .GetAllAsync(ab => ab.BookId == id)).ToList();
            authorBooks.ForEach(ab => ab.IsDeleted = isDeleted);
            await unitOfWork.AuthorBookRepository.UpdateRangeAsync(authorBooks);

            return book;
        }
    }
}
