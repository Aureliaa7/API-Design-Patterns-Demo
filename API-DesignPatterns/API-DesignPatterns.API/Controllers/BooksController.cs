using API_DesignPatterns.API.Filters;
using API_DesignPatterns.Core.DomainEntities;
using API_DesignPatterns.Core.DTOs;
using API_DesignPatterns.Core.Interfaces.DomainServices;
using API_DesignPatterns.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_DesignPatterns.API.Controllers
{
    public class BooksController : APIDesignPatternsController
    {
        private readonly IBookService bookService;
        private readonly IMapper mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            this.bookService = bookService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AddBookDto bookDto)
        {
            var addedBook = await bookService.AddAsync(mapper.Map<AddBookModel>(bookDto));
            return Ok(addedBook);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistenceFilter<Book>))]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool validateOnly)
        {
            await bookService.DeleteAsync(id, validateOnly);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookModels = await bookService.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<BookDto>>(bookModels));
        }

        [HttpGet]
        [Route("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistenceFilter<Book>))]
        [ServiceFilter(typeof(ValidateNotSoftDeletedEntityFilter<Book>))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var bookModel = await bookService.GetByIdAsync(id);
            return Ok(mapper.Map<BookDto>(bookModel));
        }

        [Authorize]
        [HttpPut]
        [Route("{id}/mark-as-deleted")]
        [ServiceFilter(typeof(ValidateEntityExistenceFilter<Book>))]
        [ServiceFilter(typeof(ValidateNotSoftDeletedEntityFilter<Book>))]
        public async Task<IActionResult> MarkAsDeleted([FromRoute] Guid id)
        {
            await bookService.MarkAsDeletedAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpPut]
        [Route("{id}/restore")]
        [ServiceFilter(typeof(ValidateEntityExistenceFilter<Book>))]
        [ServiceFilter(typeof(ValidateSoftDeletedEntityFilter<Book>))]
        public async Task<IActionResult> Restore([FromRoute] Guid id, [FromQuery] bool validateOnly)
        {
            var bookModel = await bookService.RestoreAsync(id, validateOnly);
            return Ok(mapper.Map<BookDto>(bookModel));
        }
    }
}
