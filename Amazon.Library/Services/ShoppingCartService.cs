using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;

namespace Amazon.Library.Services
{
    public class ShoppingCartService
    {
        private static ShoppingCartService? instance;

        private static object instanceLock = new object();

        public ReadOnlyCollection<ShoppingCart> carts;
        
         public ShoppingCart Cart
        {
            get
            {
                if(carts == null || !carts.Any())
                {
                    return new ShoppingCart();
                }
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }

        private ShoppingCartService() { }

        public static ShoppingCartService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }
                return instance;
            }
        }

        public ShoppingCart AddorUpdate(ShoppingCart c)
        {
            if (carts == null)
            {
                carts = new ReadOnlyCollection<ShoppingCart>(new List<ShoppingCart> { c });
                return c;
            }

            var existingCart = carts.FirstOrDefault(cart => cart.Id == c.Id);

            if (existingCart != null)
            {
                // Update existing cart properties
                existingCart.Id = c.Id;
                existingCart.Contents = c.Contents;
                // Update other properties as necessary
            }
            else
            {
                // Add new cart
                var cartList = carts.ToList();
                cartList.Add(c);
                carts = new ReadOnlyCollection<ShoppingCart>(cartList);
            }

            return c;
        }
        public void AddToCart(Item newItem)
        {
            if(Cart == null || Cart.Contents == null)
            {
                return;
            }
            var existingItem = Cart?.Contents?.FirstOrDefault(existingItem => existingItem.Id == newItem.Id);
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(invItem => invItem.Id == newItem.Id);
            if(inventoryItem == null)
            {
                return;
            }
            inventoryItem.Quantity -= newItem.Quantity;
            if (existingItem != null) 
            {
                // update
                existingItem.Quantity += newItem.Quantity;
            }
            else
            {
                //add
                Cart.Contents.Add(newItem);
            }
        }
    }


}