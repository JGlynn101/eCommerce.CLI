using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.DTO;

namespace Amazon.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal TaxRate { get; set; }
        public List<ItemDTO> Contents { get; set; }

        public ShoppingCart()
        {
            Contents = new List<ItemDTO>();
            TaxRate = 0.07m;
        }

    }
}
