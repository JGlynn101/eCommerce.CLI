using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;
using Amazon.Library.Services;


namespace AmazonApp.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
            if (Selecteditem?.Item == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Item?itemId={Selecteditem.Item.Id}");
            InventoryServiceProxy.Current.AddorUpdate(Selecteditem.Item);
        }
        public void RefreshItems()
        {
            NotifyPropertyChanged("Items");
        }
        public InventoryManagementViewModel() { 
        } 
    }
}
