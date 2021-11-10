using API_DesignPatterns.API.HttpActionResults;
using API_DesignPatterns.Core.Interfaces;
using API_DesignPatterns.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace API_DesignPatterns.API.Filters
{
    public class EntityNotSoftDeletedFilter<T> : IActionFilter where T : class, IEntity
    {
        private readonly AppDbContext dbContext;

        public EntityNotSoftDeletedFilter(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        { 
            Guid id = Guid.Empty;
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (Guid)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            var isNotSoftDeleted = dbContext.Set<T>().Where(x => x.Id.Equals(id) && !x.IsDeleted).Any();
            if (isNotSoftDeleted)
            {
                context.Result = new PreconditionFailedResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
