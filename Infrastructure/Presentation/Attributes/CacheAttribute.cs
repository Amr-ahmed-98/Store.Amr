using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;

namespace Presentation.Attributes
{
    public class CacheAttribute(int durationInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cacheService;

            var cacheKey = GenerateCacheKey(context.HttpContext.Request);

           var result = await cacheService.GetCacheValueAsync(cacheKey);

            if(!string.IsNullOrEmpty(result))
            {
                // Return Cached Response
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result
                };
                return;
            }

            // Execute the endpoint
            var contextResult = await next.Invoke();
            if(contextResult.Result is OkObjectResult okObject)
            {
                await cacheService.SetCacheValueAsync(cacheKey, okObject.Value, TimeSpan.FromSeconds(durationInSec));
            }
        }


        private string GenerateCacheKey(HttpRequest request) 
        {
            var Key = new StringBuilder();
            Key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                Key.Append($"|{item.Key}-{item.Value}"); // this the shape of query params that i want append
            }
            // /api/Products?brandId=1&typeid=1
            // /api/Products|brandId-1|typeid-1
            return Key.ToString();
        }


    }
}
