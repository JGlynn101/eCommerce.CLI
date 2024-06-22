using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Library.Models;
using Amazon.Library.Services;

namespace AmazonApp.ViewModels
{
    public class ItemViewModel
    {
        public ICommand? EditCommand { get; private set; }

        public Item? Item;
         
        public string? Name
        {
            get
            {
                return Item?.Name ?? string.Empty; 
            }
            set
            {
                if(Item != null)
                {
                    Item.Name  = value;
                }
            }
        }
        public string? Description
        {
            get
            {
                return Item?.Description ?? string.Empty;
            }
            set
            {
                if (Item != null)
                {
                    Item.Description = value;
                }
            }
        }
        public decimal Price
        {
            get
            {
                return Item?.Price ?? 0m;
            }
            set
            {
                if (Item != null)
                {
                    Item.Price = value;
                }
            }
        }
        public int Quantity
        {
            get
            {
                return Item?.Quantity ?? 0;
            }
            set
            {
                if (Item != null)
                {
                    Item.Quantity = value;
                }
            }
        }
        public int Id
        {
            get
            {
                return Item?.Id ?? 0;
            }
            set
            {
                if (Item != null)
                {
                    Item.Id = value;
                }
            }
        }
        private void ExecuteEdit(ItemViewModel? p)
        {
            if (p?.Item == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Item?itemId={p.Item.Id}");

        }
        public void Add()
        {
            InventoryServiceProxy.Current.AddorUpdate(Item);
        }
        public void SetupCommands()
        {
            EditCommand = new Command((i) => ExecuteEdit(i as ItemViewModel));
        }

        public ItemViewModel()
        {
            Item = new Item();
            SetupCommands();
        }
        public ItemViewModel(int id)
        {
            Item = InventoryServiceProxy.Current.Items.FirstOrDefault(i=> i.Id == id);   
            if(Item == null)
            {
                Item = new Item(); 
            }
        }
        public ItemViewModel(Item i) 
        {
            Item = i;
            SetupCommands();
        }
        public string? Display
        {
            get
            {
                return ToString();
            }
        }
    }
}
 