using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Amazon.Library.DTO;

namespace Amazon.Library.Models
{
    public class Item
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool BOGO { get; set; }
        public decimal Discount { get; set; }

        public Item()
        {

        }
       public Item(ItemDTO d) 
       {
            Name = d.Name;
            Description = d.Description;
            Price = d.Price;
            Id = d.Id;
            Quantity = d.Quantity;
       }
        public override string ToString()
        {
            return $"[{Id}] {Name} - {Price}";
        }   
    }
}
