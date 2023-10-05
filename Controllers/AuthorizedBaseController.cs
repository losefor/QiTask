using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QiTask.Filters;

namespace QiTask.Controllers;

[Authorize]
[ActionFilter]
[ApiController]
[Route("[controller]")]
public abstract class AuthorizedBaseController : ControllerBase
{
    public int UserId { get; set; }
    
}