using API_DesignPatterns.API.Filters;
using API_DesignPatterns.Core.DomainEntities;
using API_DesignPatterns.Core.DTOs;
using API_DesignPatterns.Core.Interfaces.DomainServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_DesignPatterns.API.Controllers
{
    public class AuthorsController : APIDesignPatternsController
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            this.authorService = authorService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddAuthorDto authorDto)
        {
            var addedAuthor = await authorService.AddAsync(mapper.Map<Author>(authorDto), authorDto.ValidateOnly);
            return Ok(mapper.Map<AuthorDto>(addedAuthor));
        }

        // Note: Since the user should not be 'forced' to use request validation, let the
        //      validateOnly value be taken from query params if it is present there,
        //      such that request validation can be applied only if the user wants to.
        [HttpPut]
        [Route("{id}/mark-as-deleted")]
        [ServiceFilter(typeof(ValidateEntityExistenceFilter<Author>))]
        [ServiceFilter(typeof(ValidateNotSoftDeletedEntityFilter<Author>))]
        public async Task<IActionResult> MarkAsDeleted([FromRoute] Guid id, [FromQuery] bool validateOnly)
        {
            await authorService.MarkAsDeletedAsync(id, validateOnly);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistenceFilter<Author>))]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool validateOnly)
        {
            await authorService.DeleteAsync(id, validateOnly);
            return NoContent();
        }

        [HttpPut]
        [Route("{id}/restore")]
        [ServiceFilter(typeof(ValidateEntityExistenceFilter<Author>))]
        [ServiceFilter(typeof(ValidateSoftDeletedEntityFilter<Author>))]
        public async Task<IActionResult> Restore([FromRoute] Guid id, [FromQuery] bool validateOnly)
        {
            var restoredAuthor = await authorService.RestoreAsync(id, validateOnly);
            return Ok(mapper.Map<AuthorDto>(restoredAuthor));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await authorService.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet]
        [Route("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistenceFilter<Author>))]
        [ServiceFilter(typeof(ValidateNotSoftDeletedEntityFilter<Author>))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var author = await authorService.GetByIdAsync(id);
            return Ok(mapper.Map<AuthorDto>(author));
        }
    }
}
