using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApplication.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UsersController : Controller
    {


        public IActionResult Get()
        {
            var users = new string[]{
            "Maarten van Stam",
            "Marcel Meijer",
            "Fanie Reynders"
        };

            return Ok(users);
        }
    }
}