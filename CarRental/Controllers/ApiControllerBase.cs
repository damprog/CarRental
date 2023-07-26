using System;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    //[Route("[controller")]
    [Route("[[api/account]]")]
    public class ApiControllerBase : Controller
    {
        protected Guid UserId => User?.Identity?.IsAuthenticated == true ? 
            Guid.Parse(User.Identity.Name) : 
            Guid.Empty;
    }
}
