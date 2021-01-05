using System;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resutContext = await next();

            if(!resutContext.HttpContext.User.Identity.IsAuthenticated) return;

            var username = resutContext.HttpContext.User.GetUsername();
            var repo = resutContext.HttpContext.RequestServices.GetService<IUserRepository>();

            var user = await repo.GetUserByUsernameAsync(username);
            user.LastActive = DateTime.Now;
            await repo.SaveAllAsync();
        }
    }
}