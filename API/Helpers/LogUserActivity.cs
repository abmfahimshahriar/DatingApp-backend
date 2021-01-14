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

            var userId = resutContext.HttpContext.User.GetUserId();
            var uow = resutContext.HttpContext.RequestServices.GetService<IUnitOfWork>();

            var user = await uow.UserRepository.GetUserByIdAsync(userId);
            user.LastActive = DateTime.UtcNow;
            await uow.Complete();
        }
    }
}