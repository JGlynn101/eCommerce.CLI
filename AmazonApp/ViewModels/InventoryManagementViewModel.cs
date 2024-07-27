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
                return InventoryServiceProxy.Current.Items.Where(i=>i != null).Select(i => new ItemViewModel(i)).ToList() ?? new List<ItemViewModel>();
            } 
                
        }
        public ItemViewModel Selecteditem { get; set; } 
        public void updateItem()
        {
            if (Selecteditem?.Model == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Item?itemId={Selecteditem.Model.Id}");
            InventoryServiceProxy.Current.AddorUpdate(Selecteditem.Model);
        }
        public void DeleteItem()
        {
            if (Selecteditem?.Model == null)
            {
                return;
            }
            InventoryServiceProxy.Current.Delete(Selecteditem.Model.Id);
            RefreshItems();

        }
        public void RefreshItems()
        {
            NotifyPropertyChanged("Items");
        }
        public InventoryManagementViewModel() { 
        } 
    }
}
