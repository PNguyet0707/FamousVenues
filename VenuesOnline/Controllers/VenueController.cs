using DataLayer.Dtos;
using DataLayer.Dtos.Request;
using DataLayer.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace FamousVenues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController(IVenueService venueService, IWebHostEnvironment env) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("createvenue")]
        public async Task<ActionResult<VenueCreationResponse>> CreateVenue(VenueCreationRequest request)
        {
            var result = await venueService.CreateVenue(request, env.ContentRootPath);
            if (result is null)
                return BadRequest("Failed to create a venue.");
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("updatevenue")]
        public async Task<ActionResult<VenueCreationResponse>> UpdateVenue(VenueUpdateRequest request)
        {
            var result = await venueService.UpdateVenue(request, env.ContentRootPath);
            if (result)
                return Ok(result);
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("deletevenuebyid")]
        public async Task<ActionResult> DeleteVenueById(Guid venueId)
        {
            var result = await venueService.DeleteVenueById(venueId, env.ContentRootPath);
            if (!result)
                return NotFound();
            return  NoContent();
        }
        [HttpPost("getvenuesbybounding")]
        public async Task<ActionResult<List<VenueResponse>>> GetVenue(VenuesRequest request)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var result = await venueService.GetVenues(request, baseUrl);
            if (result is null || result.Count == 0)
            {
                return NotFound("Not found");
            }
            return Ok(result);
        }

        [HttpPost("ratevenue")]
        public async Task<ActionResult<VenueRatingResponse>> RateVenue(RateVenueRequest request)
        {
            var result = await venueService.RateVenueAsync(request);
            if (result is null)
                return BadRequest("Failed to rate venue");
            return Ok(result);
        }
    }
}
