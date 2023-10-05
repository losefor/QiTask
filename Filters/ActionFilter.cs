using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using QiTask.Controllers;

namespace QiTask.Filters;

public class ActionFilter : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    // Pull the user ID on each request
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var c = context.Controller as AuthorizedBaseController;
        c.UserId = int.Parse(c.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}