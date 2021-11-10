using API_DesignPatterns.Core.DomainEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.Interfaces.DomainServices
{
    public interface IAuthorService
    {
        /// <summary>
        /// Adds an author
        /// </summary>
        /// <param name="author">An object containing the author details</param>
        /// <param name="validateOnly">Flag used for request validation</param>
        /// <returns>The created author</returns>
        Task<Author> AddAsync(Author author, bool validateOnly = false);

        /// <summary>
        /// Gets an author by id if the IsDeleted field is false
        /// </summary>
        /// <param name="id">The author id</param>
        /// <returns>The searched author</returns>
        Task<Author> GetByIdAsync(Guid id);

        /// <summary>
        /// Returns all the authors
        /// </summary>
        /// <returns>An IEnumerable<Author></returns>
        Task<IEnumerable<Author>> GetAllAsync();

        /// <summary>
        /// Soft deletes an author by id
        /// </summary>
        /// <param name="id">The author id</param>
        /// <param name="validateOnly">Flag indicating if the performed changes should be saved 
        /// or the user wants to validateOnly</param>
        /// <returns></returns>
        Task MarkAsDeletedAsync(Guid id, bool validateOnly = false);

        /// <summary>
        /// Expunges an author by id
        /// </summary>
        /// <param name="id">The author id</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, bool validateOnly = false);

        /// <summary>
        /// Restores an author
        /// </summary>
        /// <param name="id">The author id</param>
        /// <returns>The restored author</returns>
        Task<Author> RestoreAsync(Guid id, bool validateOnly = false);
    }
}
