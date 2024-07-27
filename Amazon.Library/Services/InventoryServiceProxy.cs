using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;

namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<Item> items;
        public List<Item> Items 
        {
            get
            {
                return items;
            }
        }

        private int NextId
        {
            get
            {
                if (!Items.Any())
                {
                    return 1;
                }
                return items.Select(i => i.Id).Max() + 1;
            }
        }
        public Item? AddorUpdate(Item? i)
        {
            if (items == null)
            {
                return null;
            }
            bool isAdd = false;
            if(i.Id == 0)
            {
                isAdd = true;
                i.Id = NextId;
            }
            if(isAdd) 
            {
                items.Add(i);
            }
            if (!isAdd)
            {
                var item_toUpdate = items.FirstOrDefault(item  => item.Id == i.Id);
                item_toUpdate = i;
            }
              
                return i;
        }

        private InventoryServiceProxy()
        {
            items = new List<Item>() {
                new Item { Id = 1, Name = "Item 1", Price = 10.0m, Description = "Great Product" , Quantity = 40 },
            new Item { Id = 2, Name = "Item 2", Price = 20.0m, Quantity = 12},
            new Item { Id = 3, Name = "Item 3", Price = 30.0m , Quantity = 23}
            };
        }
        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                } 
                return instance;
            }
        }
        public void Delete(int id)
         {
            if(items == null)
            {
                return;
            }
            var itemToDelete = items.FirstOrDefault(i => i.Id == id);
            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
        }
    }
}
