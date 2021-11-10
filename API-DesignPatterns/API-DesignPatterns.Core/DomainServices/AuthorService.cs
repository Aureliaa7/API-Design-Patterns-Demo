using API_DesignPatterns.Core.DomainEntities;
using API_DesignPatterns.Core.Exceptions;
using API_DesignPatterns.Core.Interfaces.DomainServices;
using API_DesignPatterns.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.DomainServices
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Author> AddAsync(Author author, bool validateOnly = false)
        {
            bool authorExists = await unitOfWork.AuthorRepository.ExistsAsync(
                a => a.FirstName == author.FirstName && a.LastName == author.LastName);
            if (authorExists)
            {
                throw new DuplicatedAuthorException($"An author named {author.FirstName} {author.LastName} already exists!");
            }

            var addedAuthor = await unitOfWork.AuthorRepository.AddAsync(author);

            if (!validateOnly)
            {
                await unitOfWork.SaveChangesAsync();
            }

            return addedAuthor;
        }

        public async Task MarkAsDeletedAsync(Guid id, bool validateOnly = false)
        {
            var author = await unitOfWork.AuthorRepository.GetByIdAsync(id);
            author.IsDeleted = true;
            await unitOfWork.AuthorRepository.UpdateAsync(author);

            // also "delete" all the related entities
            var books = (await unitOfWork.AuthorBookRepository.GetAllAsync(ab => ab.AuthorId == id))
                .Select(ab => ab.Book).ToList();
            books.ForEach(b => b.IsDeleted = true);
            await unitOfWork.BookRepository.UpdateRangeAsync(books);

            var authorBooks = (await unitOfWork.AuthorBookRepository.GetAllAsync(ab => ab.AuthorId == id)).ToList();
            authorBooks.ForEach(ab => ab.IsDeleted = true);
            await unitOfWork.AuthorBookRepository.UpdateRangeAsync(authorBooks);

            if (!validateOnly)
            {
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            var author = await unitOfWork.AuthorRepository
                .GetFirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

            if(author == null)
            {
                throw new EntityNotFoundException($"The author with the id {id} was not found!");
            }

            return author;
        }

        public async Task DeleteAsync(Guid id, bool validateOnly = false)
        {
            await unitOfWork.AuthorRepository.RemoveAsync(id);
            if (!validateOnly)
            {
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<Author> RestoreAsync(Guid id, bool validateOnly = false)
        {
            var author = await unitOfWork.AuthorRepository.GetByIdAsync(id);
            author.IsDeleted = false;
            await unitOfWork.AuthorRepository.UpdateAsync(author);

            // also restore all the related entities
            var books = (await unitOfWork.AuthorBookRepository.GetAllAsync(ab => ab.AuthorId == id))
                .Select(ab => ab.Book).ToList();
            books.ForEach(b => b.IsDeleted = false);
            await unitOfWork.BookRepository.UpdateRangeAsync(books);

            var authorBooks = (await unitOfWork.AuthorBookRepository.GetAllAsync(ab => ab.AuthorId == id)).ToList();
            authorBooks.ForEach(ab => ab.IsDeleted = false);
            await unitOfWork.AuthorBookRepository.UpdateRangeAsync(authorBooks);

            if (!validateOnly)
            {
                await unitOfWork.SaveChangesAsync();
            }

            return author;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return (await unitOfWork.AuthorRepository.GetAllAsync(a => !a.IsDeleted)).ToList();
        }
    }
}
