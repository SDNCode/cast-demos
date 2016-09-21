using Microsoft.AspNetCore.Mvc;

namespace ConsoleApplication.Controllers
{

    [Route("[Controller]")]
    public class CustomersController : Controller
    {
        public IActionResult Get()
        {
            var customers = new string[]{
            "Sheilah Cosner",
            "Marisa Gariepy",
            "Nichelle Chamblee",
            "Karima Routon",
            "Wilhelmina Singh",
            "Tom Mccurdy"
        };

            return Ok(customers);
        }
    }
}