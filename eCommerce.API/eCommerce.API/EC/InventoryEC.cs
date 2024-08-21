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
        public async Task<IEnumerable<ItemDTO>> Get()
        {
            return Filebase.Current.Items.Take(100).Select(i => new ItemDTO(i));
        }
        public async Task<IEnumerable<ItemDTO>> Search(string? query)
        {
            return Filebase.Current.Items.Where(i => 
            (i?.Name != null && i.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty)) 
                || 
            (i?.Description != null && i.Description.ToUpper().Contains(query?.ToUpper() ?? string.Empty)))
                .Take(100).Select(i => new ItemDTO(i));  
        }
        public async Task<ItemDTO?> Delete(int id)
        {
            return new ItemDTO(Filebase.Current.Delete(id));
                
            //    FirstOrDefault(i => i.Id == id);
            //if (itemToDelete == null)
            //{
            //    return null;
            //}
            //Filebase.Current.Items.Remove(itemToDelete);
            //return new ItemDTO(itemToDelete);
        }
        public async Task<ItemDTO> AddOrUpdate(ItemDTO i)
        {
            //if (Filebase.Current.Items == null)
            //{
            //    return null;
            //}
            //bool isAdd = false;
            //if (i.Id == 0)
            //{
            //    isAdd = true;
            //    i.Id = Filebase.Current.NextItemId;
            //}
            //if (isAdd)
            //{
            //    Filebase.Current.Items.Add(new Item(i));
            //}
            //if (!isAdd)
            //{
            //    var item_toUpdate = Filebase.Current.Items.FirstOrDefault(item => item.Id == i.Id);
            //    if(item_toUpdate != null)
            //    {
            //        var index = Filebase.Current.Items.IndexOf(item_toUpdate);
            //        Filebase.Current.Items.RemoveAt(index);
            //        item_toUpdate = new Item(i);
            //        Filebase.Current.Items.Insert(index, item_toUpdate);

            //    }
            //}

            //return i;
           return new ItemDTO(Filebase.Current.AddOrUpdate(new Item(i)));
        }
    }   
}
