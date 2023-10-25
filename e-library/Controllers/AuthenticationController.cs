using e_library.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_library.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpGet(Name = "GetUser")]
    public IEnumerable<User> Get()
    {
        using (var context = new NetCoreAuthenticationContext()) 
        {
            //Get All Users
            return context.Users.ToList();
        }
    } 
}
