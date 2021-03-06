using System;
using System.Threading.Tasks;
using Sixgram.Auth.Common.Error;
using Sixgram.Auth.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace Sixgram.Auth.Api.Controllers
{
    public class BaseController : ControllerBase    
    {
        protected async Task<ActionResult> ReturnResult<T, TM>(Task<T> task) where T : ResultContainer<TM>
        {
            var result = await task;

            if (result.ErrorType.HasValue)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(),
                    ErrorType.BadRequest => BadRequest(),
                    ErrorType.Unauthorized => Unauthorized(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            if (result.Data == null)
                return NoContent();

            return Ok(result.Data);
        }
    }
}