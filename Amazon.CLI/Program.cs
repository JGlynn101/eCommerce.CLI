using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Inventory Management");
                Console.WriteLine("2. Shop");
                Console.WriteLine("0. Exit");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        InventoryManagement();
                        break;
                    case "2":
                        Shop();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void InventoryManagement()
        {
            while (true)
            {
                Console.WriteLine("Inventory Management:");
                Console.WriteLine("1. Create Item");
                Console.WriteLine("2. Read Items");
                Console.WriteLine("3. Update Item");
                Console.WriteLine("4. Delete Item");
                Console.WriteLine("0. Back to Main Menu");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateItem();
                        break;
                    case "2":
                        ReadItems();
                        break;
                    case "3":
                        UpdateItem();
                        break;
                    case "4":
                        DeleteItem();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CreateItem()
        {
            Console.WriteLine("Enter item name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter item description:");
            string description = Console.ReadLine();

            Console.WriteLine("Enter item price:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter item quantity:");
            int quantity = int.Parse(Console.ReadLine());

            var item = new Item
            {
                Name = name,
                Description = description,
                Price = price,
                Quantity = quantity
            };

            InventoryServiceProxy.Current.AddorUpdate(item);

            Console.WriteLine("Item created successfully!");
        }

        static void ReadItems()
        {
            var items = InventoryServiceProxy.Current.Items;

            if (!items.Any())
            {
                Console.WriteLine("No items found.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Description: {item.Description}, Price: {item.Price}, Quantity: {item.Quantity}");
            }
        }

        static void UpdateItem()
        {
            Console.WriteLine("Enter item ID to update:");
            int id = int.Parse(Console.ReadLine());

            var item = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            Console.WriteLine("Enter new item name:");
            item.Name = Console.ReadLine();

            Console.WriteLine("Enter new item description:");
            item.Description = Console.ReadLine();

            Console.WriteLine("Enter new item price:");
            item.Price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter new item quantity:");
            item.Quantity = int.Parse(Console.ReadLine());

            InventoryServiceProxy.Current.AddorUpdate(item);

            Console.WriteLine("Item updated successfully!");
        }

        static void DeleteItem()
        {
            Console.WriteLine("Enter item ID to delete:");
            int id = int.Parse(Console.ReadLine());

            InventoryServiceProxy.Current.Delete(id);

            Console.WriteLine("Item deleted successfully!");
        }

        static void Shop()
        {
            while (true)
            {
                Console.WriteLine("Shop:");
                Console.WriteLine("1. Add Item to Cart");
                Console.WriteLine("2. Remove Item from Cart");
                Console.WriteLine("3. Checkout");
                Console.WriteLine("0. Back to Main Menu");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddItemToCart();
                        break;
                    case "2":
                        RemoveItemFromCart();
                        break;
                    case "3":
                        Checkout();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddItemToCart()
        {
            Console.WriteLine("Enter item ID to add to cart:");
            int id = int.Parse(Console.ReadLine());

            var item = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            Console.WriteLine("Enter quantity:");
            int quantity = int.Parse(Console.ReadLine());

            if (quantity > item.Quantity)
            {
                Console.WriteLine("Not enough items in stock.");
                return;
            }

            var cartItem = new Item
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Quantity = quantity
            };

            ShoppingCartService.Current.AddToCart(cartItem);

            Console.WriteLine("Item added to cart.");
        }

        static void RemoveItemFromCart()
        {
            Console.WriteLine("Enter item ID to remove from cart:");
            int id = int.Parse(Console.ReadLine());

            var cart = ShoppingCartService.Current.Cart;
            var item = cart.Contents.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                Console.WriteLine("Item not found in cart.");
                return;
            }

            cart.Contents.Remove(item);

            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Id == id);
            inventoryItem.Quantity += item.Quantity;

            Console.WriteLine("Item removed from cart.");
        }

        static void Checkout()
        {
            var cart = ShoppingCartService.Current.Cart;
            if (!cart.Contents.Any())
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            decimal subtotal = cart.Contents.Sum(i => i.Price * i.Quantity);
            decimal taxes = subtotal * 0.07m;
            decimal total = subtotal + taxes;

            Console.WriteLine("Receipt:");
            foreach (var item in cart.Contents)
            {
                Console.WriteLine($"Name: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}, Total: {item.Price * item.Quantity}");
            }
            Console.WriteLine($"Subtotal: {subtotal}");
            Console.WriteLine($"Taxes: {taxes}");
            Console.WriteLine($"Total: {total}");

            cart.Contents.Clear();
            Console.WriteLine("Checkout complete.");
        }
    }
}
