using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;
using Amazon.Library.DTO;
using Amazon.Library.Services;


namespace AmazonApp.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public string? Query { get; set; }   
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
        public async void DeleteItem()
        {
            await InventoryServiceProxy.Current.Delete(Selecteditem.Model?.Id ?? 0);
            Refresh();
        }
        public async void Search()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            Refresh();
        }
        public async void Refresh()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            NotifyPropertyChanged(nameof(Items));
        }
        public InventoryManagementViewModel() { 
        } 
    }
}
