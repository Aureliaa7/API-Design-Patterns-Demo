using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API_DesignPatterns.API.HttpActionResults
{
    public class PreconditionFailedResult : StatusCodeResult
    {
        public PreconditionFailedResult([ActionResultStatusCode] int statusCode = 412) : base(statusCode)
        {
        }
    }
}
