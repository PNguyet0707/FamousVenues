using Microsoft.AspNetCore.Mvc;

namespace FamousVenues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDeadLockController : ControllerBase
    {
        [HttpGet] 
        public void DeadLockWhenUsingGetResult()
        {
            var result = GetDataAsync().Result;
            Console.WriteLine(result);
        }
        private async Task<string> GetDataAsync()
        {
            await Task.Delay(5000);
            return "Hello";
        }
    }
}
