using Microsoft.AspNetCore.Mvc;
using SBSender.Services.Interfaces;
using SBShared.Models;

namespace SBSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SBSenderController(IQueueService queueService) : Controller
    {
        [HttpPost("sendmessage")]
        public async Task<ActionResult<bool>> PublishMessage(PersonModel person)
        {
            if (person is null)
            {
                return false;   
            }
            await queueService.SendMessageAsync(person);
            return true;
        }
        [HttpPost("sendmessages")]
        public async Task<ActionResult<bool>> PublishMessages(List<PersonModel> persons)
        {
            if (persons is null || ! persons.Any())
            {
                return false;
            }
            await queueService.SendBatchMessageAsync(persons);
            return true;
        }
        [HttpPost("sendmessagetotopic")]
        public async Task<bool> SendMessageToTopic(PersonModel person)
        {
            if (person is null) 
                return false;
            await queueService.SendMessageToTopicAsync(person);
            return true;
        }
    }
}
