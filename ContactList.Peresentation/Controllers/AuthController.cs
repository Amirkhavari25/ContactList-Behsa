using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Peresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
