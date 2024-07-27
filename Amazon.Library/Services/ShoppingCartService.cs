using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Amazon.Library.Models;

namespace Amazon.Library.Services
{
    public class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static readonly object instanceLock = new object();
        private readonly List<ShoppingCart> _carts;

        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get
            {
                return _carts.AsReadOnly();
            }
        }

        private ShoppingCartService()
        {
            _carts = new List<ShoppingCart>
            {
                new ShoppingCart { Id = 1 , Name = "Shopping Cart" }
            };
        }

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

        public ShoppingCart GetCart(int cartId)
        {
            return _carts.FirstOrDefault(cart => cart.Id == cartId) ?? new ShoppingCart { Id = cartId };
        }
        private int NextId
        {
            get
            {
                if (!_carts.Any())
                {
                    return 1;
                }
                return _carts.Select(c => c.Id).Max() + 1;
            }
        }
        public ShoppingCart? AddOrUpdateCart(ShoppingCart cart)
        {
            if (_carts == null || cart == null)
            {
                return null;
            }
            bool isAdd = false;
            if (cart.Id == 0)
            {
                isAdd = true;
                cart.Id = NextId;
            }
            if (isAdd)
            {
                _carts.Add(cart);
            }
            else
            {
                var cartToUpdate = _carts.FirstOrDefault(c => c.Id == cart.Id);
                if (cartToUpdate != null)
                {
                    cartToUpdate.Name = cart.Name;
                    cartToUpdate.Contents = cart.Contents;
                }
            }
            return cart;
        }

        public ShoppingCart FindCart(int cartId)
        {
            return _carts.FirstOrDefault(c => c.Id == cartId);
        }

        public void AddToCart(int cartId, Item newItem)
        {
            var cart = GetCart(cartId);
            var existingItem = cart?.Contents?.FirstOrDefault(item => item.Id == newItem.Id);
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(invItem => invItem.Id == newItem.Id);
            if (inventoryItem == null)
            {
                return;
            }
            if(newItem.BOGO)
            {
                newItem.Quantity *= 2;
            }
            if (inventoryItem.Quantity <= newItem.Quantity)
            {
                newItem.Quantity = inventoryItem.Quantity;
            }
            if (existingItem != null)
            {
                if (inventoryItem.Quantity > (newItem.Quantity + existingItem.Quantity)) 
                { 
                    existingItem.Quantity += newItem.Quantity; 
                }
                else
                    existingItem.Quantity = inventoryItem.Quantity;
            }
            else
            {
                cart?.Contents?.Add(newItem);
            }

            AddOrUpdateCart(cart);
        }
        public void RemoveItemFromCart(int cartId, Item i)
        {
            var cart = GetCart(cartId);
            var existingItem = cart?.Contents?.FirstOrDefault(item => item.Id == i.Id);
            var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(invItem => invItem.Id == i.Id);
            if (i == null)
            {
                return;
            }

        }
        public void RemoveCart(ShoppingCart cart)
        {
            if (_carts.Contains(cart))
            {
                _carts.Remove(cart);
            }
        }
        public void UpdateTaxRate(int cartId, decimal newRate)
        {
            var cart = GetCart(cartId);
            if (cart != null)
            {
                cart.TaxRate = newRate;
                AddOrUpdateCart(cart);
            }
        }
    }
}
