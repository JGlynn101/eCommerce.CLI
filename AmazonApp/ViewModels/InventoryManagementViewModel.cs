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
        public List<Amazon.Library.Models.Item> Items
        {
            get
            {
                return InventoryServiceProxy.Current.Items.ToList() ?? new List<Item>();
            } 
                
        }
        public Amazon.Library.Models.Item Selecteditem { get; set; } 
        public void updateItem()
        {
            InventoryServiceProxy.Current.AddorUpdate(Selecteditem);
        }
        public InventoryManagementViewModel() { 
        } 
    }
}
 