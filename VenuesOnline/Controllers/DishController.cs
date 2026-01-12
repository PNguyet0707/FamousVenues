using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;

namespace FamousVenues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController(IDishService dishService) : ControllerBase
    {
        [HttpPost("createdish")]
        public async Task<ActionResult<DishCreationResponse>> CreateDish(DishCreationRequest request)
        {
            var result = await dishService.CreateDish(request);
            if (result is null)
                return BadRequest("Failed to create a dish.");
            return Ok(result);
        }
        [HttpGet("getalldish")]
        public async Task<ActionResult<List<DishCreationResponse>>>GetAllDish()
        {
            var result = await dishService.GetAllDishes();
            return result;
           
        }
        [HttpGet("testconnectionexception")]
        public ActionResult TestConnectionException()
        {
            var result = 0;
            if(result == 0)
            {
                throw new ConnectionException();
            }
            else
            {
                throw new ApplicationException();
            }
        }
    }
}