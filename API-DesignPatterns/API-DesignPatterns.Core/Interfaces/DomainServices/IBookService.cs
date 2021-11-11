using API_DesignPatterns.Core.DomainEntities;
using API_DesignPatterns.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.Interfaces.DomainServices
{
    public interface IBookService
    {
        /// <summary>
        /// Adds a book
        /// </summary>
        /// <param name="book">An object containing the book details</param>
        /// <returns>The created book</returns>
        Task<Book> AddAsync(AddBookModel book);

        /// <summary>
        /// Gets a book by id if the IsDeleted field is false
        /// </summary>
        /// <param name="id">The book id</param>
        /// <returns>The searched book</returns>
        Task<BookModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Returns all the books
        /// </summary>
        /// <returns>An IEnumerable<BookModel></returns>
        Task<IEnumerable<BookModel>> GetAllAsync();

        /// <summary>
        /// Soft deletes a book by id
        /// </summary>
        /// <param name="id">The book id</param>
        /// <param name="validateOnly">Flag used for request validation</param>
        /// <returns></returns>
        Task MarkAsDeletedAsync(Guid id, bool validateOnly = false);

        /// <summary>
        /// Expunges a book by id
        /// </summary>
        /// <param name="id">The book id</param>
        /// <param name="validateOnly">Flag used for request validation</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, bool validateOnly = false);

        /// <summary>
        /// Restores a soft deleted book
        /// </summary>
        /// <param name="id">The book id</param>
        /// <returns>The restored book</returns>
        Task<BookModel> RestoreAsync(Guid id, bool validateOnly = false);
    }
}
