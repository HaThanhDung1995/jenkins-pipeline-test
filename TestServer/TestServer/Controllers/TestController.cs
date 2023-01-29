using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6 };
        }
    }
}
