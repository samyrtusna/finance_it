using System.Security.Claims;
using Finance_it.API.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int UserId
        {
            get
            {
                var claimValue = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(claimValue, out int id))
                {
                    throw new BadRequestException("Invalid user ID in token.");
                }
                return id;
            }
        }
    }
}
