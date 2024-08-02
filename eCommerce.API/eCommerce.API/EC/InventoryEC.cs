using System.Security.Cryptography.X509Certificates;
using Amazon.Library.Models;
using Amazon.Library.DTO;
using eCommerce.API.Database;
using Microsoft.AspNetCore.Http.Features;

namespace eCommerce.API.EC
{
    public class InventoryEC
    {
        public InventoryEC() { }
        public async Task<IEnumerable<Item>> Get()
        {
            return FakeDatabase.Items.Take(100);
        }
    }
}
