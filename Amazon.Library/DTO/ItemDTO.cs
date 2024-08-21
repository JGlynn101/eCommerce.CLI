using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;

namespace Amazon.Library.DTO
{
    public class ItemDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool BOGO { get; set; } 
        public decimal Discount { get; set; }   

        public ItemDTO(Item i)
        {
            Name = i.Name; 
            Description = i.Description; 
            Price = i.Price; 
            Id = i.Id; 
            Quantity = i.Quantity;
        }
        public ItemDTO(ItemDTO i)
        {
            Name = i.Name;
            Description = i.Description;
            Price = i.Price;
            Id = i.Id;
            Quantity = i.Quantity;
        }
        public ItemDTO() { }
    }
}
