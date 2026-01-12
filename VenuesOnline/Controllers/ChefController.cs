using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using System.Threading.Tasks;

namespace FamousVenues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController(IChefService chefService) : ControllerBase
    {
        [HttpPost("createchef")]
        public async Task<ActionResult<ChefCreationResponse>> CreateChef(ChefCreationRequest request)
        {
            var result = await chefService.CreateChef(request);
            if (result is null)
                return BadRequest("Failed to create chef.");
            return CreatedAtAction(nameof(CreateChef),  result);
        }
    }
}