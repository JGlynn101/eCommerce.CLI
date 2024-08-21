using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;
using Amazon.Library.Utilities;
using Amazon.Library.DTO;
using Newtonsoft.Json;

namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<ItemDTO> items;
        public List<ItemDTO> Items 
        {
            get
            {
                return items;
            }
        }
        public async Task<IEnumerable<ItemDTO>> Get()
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult = JsonConvert.DeserializeObject<List<ItemDTO>>(result);
            items = deserializedResult.ToList() ?? new List<ItemDTO>();
            return items;
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
        public async Task<IEnumerable<ItemDTO>> Search(Query? query)
        {
            if (query == null || string.IsNullOrEmpty(query.QueryString))
            {
                return await Get();
            }
            var result = await new WebRequestHandler().Post("/Inventory/Search", query);
            items = JsonConvert.DeserializeObject<List<ItemDTO>>(result) ?? new List<ItemDTO>();
            return Items;

        }
        public async Task<ItemDTO> AddorUpdate(ItemDTO i)
        {
            var result = await new WebRequestHandler().Post("/Inventory", i);
            return JsonConvert.DeserializeObject<ItemDTO>(result);
        }
        public async Task<ItemDTO?> Delete(int id)
        {
            var response = await new WebRequestHandler().Delete($"/{id}");
            var itemToDelete = JsonConvert.DeserializeObject<ItemDTO>(response);
            return itemToDelete;
            //var itemToDelete = items.FirstOrDefault(i => i.Id == id);
            //if (itemToDelete == null)
            //{
            //    return null;
            //}
            //items.Remove(itemToDelete);
            //return itemToDelete;
        }
        private InventoryServiceProxy()
        { 
            // Make a web call
            var response = new WebRequestHandler().Get("/Inventory").Result;
            items = JsonConvert.DeserializeObject<List<ItemDTO>>(response);
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
    }
}
