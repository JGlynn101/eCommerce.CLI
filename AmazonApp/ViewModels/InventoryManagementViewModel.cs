using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;
using Amazon.Library.Services;


namespace AmazonApp.ViewModels
{
    public class InventoryManagementViewModel
    {
        public List<ItemViewModel> Items
        {
            get
            {
                return InventoryServiceProxy.Current.Items.Select(i => new ItemViewModel(i)).ToList() ?? new List<ItemViewModel>();
            } 
                
        }
        public ItemViewModel Selecteditem { get; set; } 
        public void updateItem()
        {
            if (Selecteditem.Item == null) return;
            InventoryServiceProxy.Current.AddorUpdate(Selecteditem.Item);
        }
        public InventoryManagementViewModel() { 
        } 
    }
}
