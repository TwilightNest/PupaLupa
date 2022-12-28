using Microsoft.AspNetCore.Mvc;

namespace PupaLupaServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        // GET: api
        [HttpGet]
        public bool IsAviable()
        {
            return true;
        }
    }
}