using Microsoft.AspNetCore.Mvc;
using RabbitMqService;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using System.Runtime.CompilerServices;
namespace RabbitMqProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController(IRabbitService _rabbitService) : ControllerBase
    {
        [HttpPost("publishcustomerinfo")]
        public async Task<ActionResult> PublishCustomerInfo(Customer customer) 
        {
            try
            {
                string customerName = customer.FullName;
                for(int i = 1; i <= 20; i++)
                {
                    customer.Id = Guid.NewGuid();
                    customer.FullName = $"{customerName} {i}";
                    var customerStr = JsonSerializer.Serialize(customer);
                    var message = Encoding.UTF8.GetBytes(customerStr);
                    await _rabbitService.SendMessage(message);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("publishfanoutexchangemessage")]
        public async Task<ActionResult> PublishFanoutMessage(Customer customer)
        {
            try
            {
                string customerName = customer.FullName;
                for (int i = 1; i <= 20; i++)
                {
                    customer.Id = Guid.NewGuid();
                    customer.FullName = $"{customerName} {i}";
                    var customerStr = JsonSerializer.Serialize(customer);
                    var message = Encoding.UTF8.GetBytes(customerStr);
                    await _rabbitService.SendMessageToExchange(message, RabbitExchangeType.Fanout);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("publishdirectexchangemessage")]
        public async Task<ActionResult> PublishDirectMessage(Customer customer)
        {
            try
            {
                Random random = new Random();
                int idx = random.Next(0, 100);
                string customerName = $"{customer.FullName}_{idx.ToString()}";
                customer.Id = Guid.NewGuid();
                customer.FullName = customerName;
                var customerStr = JsonSerializer.Serialize(customer);
                var message = Encoding.UTF8.GetBytes(customerStr);
                string bindingKey = customer.CustomerType switch
                {
                    CustomerType.Personal => CustomerType.Personal.ToString(),
                    CustomerType.Enterprise => CustomerType.Enterprise.ToString(),
                    _ => string.Empty
                };
                await _rabbitService.SendMessageToExchange(message, RabbitExchangeType.Direct, bindingKey);
               
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("publishtoppicexchangemessage")]
        public async Task<ActionResult> PublishToppicMessage(Customer customer)
        {
            try
            {
                Random random = new Random();
                int idx = random.Next(0, 100);
                string customerName = $"{customer.FullName}_{idx.ToString()}";
                customer.Id = Guid.NewGuid();
                customer.FullName = customerName;
                var customerStr = JsonSerializer.Serialize(customer);
                var message = Encoding.UTF8.GetBytes(customerStr);
                string customerTypeKey = customer.CustomerType switch
                {
                    CustomerType.Personal => CustomerType.Personal.ToString(),
                    CustomerType.Enterprise => CustomerType.Enterprise.ToString(),
                    _ => "*"
                };
                string bindingKey = $"{customer.Country}.{customerTypeKey}.{customer.YearOfBirth}";
                await _rabbitService.SendMessageToExchange(message, RabbitExchangeType.Topic, bindingKey);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}
