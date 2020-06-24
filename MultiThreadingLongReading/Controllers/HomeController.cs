using Microsoft.AspNetCore.Mvc;
using MultiThreadingLongReading.Services;

namespace MultiThreadingLongReading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("withoutlock")]
        public IActionResult WithoutLock()
        {
            var res = ReaderService.ReadFile();
            return Ok(res);
        }

        [Route("withlock")]
        public IActionResult WithLock()
        {
            var res = ReaderService.ReadFileWithLock();
            return Ok(res);
        }

        [Route("monitor")]
        public IActionResult Monitor()
        {
            var res = ReaderService.MonitorTest();
            return Ok(res);
        }
    }
}
