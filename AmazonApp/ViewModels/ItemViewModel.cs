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

        public Item? Model;
        public override string ToString()
        {
            if (Model == null)
            {
                return string.Empty;
            }
            return $"[{Model.Id}] {Model.Name} - {Model.Price:C}";
        }
        public string? Name
        {
            get
            {
                return Model?.Name ?? string.Empty; 
            }
            set
            {
                if(Model != null)
                {
                    Model.Name  = value;
                }
            }
        }
        public string? Description
        {
            get
            {
                return Model?.Description ?? string.Empty;
            }
            set
            {
                if (Model != null)
                {
                    Model.Description = value;
                }
            }
        }
        public decimal Price
        {
            get
            {
                return Model?.Price ?? 0m;
            }
            set
            {
                if (Model != null)
                {
                    Model.Price = value;
                }
            }
        }
        public int Quantity
        {
            get
            {
                return Model?.Quantity ?? 0;
            }
            set
            {
                if (Model != null)
                {
                    Model.Quantity = value;
                }
            }
        }
        public int Id
        {
            get
            {
                return Model?.Id ?? 0;
            }
            set
            {
                if (Model != null)
                {
                    Model.Id = value;
                }
            }
        }
        public decimal Discount
        {
            get
            {
                return Model?.Discount ?? 0;
            }
            set
            {
                if (Model != null && Model.Discount != value)
                {
                    Model.Discount = value;
                }
            }
        }
        public bool BOGO
        {
            get
            {
                return Model?.BOGO ?? false;
            }
            set
            {
                if (Model != null && Model.BOGO != value)
                {
                    Model.BOGO = value;
                }
            }
        }

        private void ExecuteEdit(ItemViewModel? p)
        {
            if (p?.Model == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Item?itemId={p.Model.Id}");

        }
        public void Add()
        {
            InventoryServiceProxy.Current.AddorUpdate(Model);
        }
        public void SetupCommands()
        {
            EditCommand = new Command((i) => ExecuteEdit(i as ItemViewModel));
        }

        public ItemViewModel()
        {
            Model = new Item();
            SetupCommands();
        }
        public ItemViewModel(int id)
        {
            Model = InventoryServiceProxy.Current.Items.FirstOrDefault(i=> i.Id == id);   
            if(Model == null)
            {   
                Model = new Item(); 
            }
        }
        public ItemViewModel(Item? i) 
        {
            if(i != null)
            {
                Model = i;
            }
            else
            {
                Model = new Item();
            }
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
 