using Amazon.Library.Models;
using eCommerce.API.EC;
using Microsoft.AspNetCore.Mvc;
using Amazon.Library.DTO;

namespace eCommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<ItemDTO>> Get()
        {
            return await new InventoryEC().Get(); 
        }

        [HttpPost("Search")]
        public async Task<IEnumerable<ItemDTO>> Get(Query query)
        {
            return await new InventoryEC().Search(query.QueryString);
        }

        [HttpPost()]
        public async Task<ItemDTO> AddOrUpdate([FromBody] ItemDTO i)
        {
            return await new InventoryEC().AddOrUpdate(i);
        }

        [HttpDelete("/{id}")]
        public async Task<ItemDTO?> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }
    }
}
