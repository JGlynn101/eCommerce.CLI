using Amazon.Library.Models;

namespace eCommerce.API.Database
{
    public static class FakeDatabase
    {
        public static int NextItemId
        {
            get
            {
                if (!Items.Any())
                {
                    return 1;
                }
                return Items.Select(i => i.Id).Max() + 1;
            }
        }
        public static List<Item> Items { get;  } = new List<Item>{
                new Item { Id = 1, Name = "Item 1", Price = 10.0m, Description = "Great Product" , Quantity = 40 },
                new Item { Id = 2, Name = "Item 2", Price = 20.0m, Quantity = 12},
                new Item { Id = 3, Name = "Item 3", Price = 30.0m , Quantity = 23},
                new Item { Id = 4, Name = "Item 4", Price = 50.0m , Quantity = 89}
        };
    }
}
