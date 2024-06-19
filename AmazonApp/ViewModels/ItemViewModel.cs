using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Library.Models;

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
        private void ExecuteEdit(ItemViewModel? p)
        {
            if(p == null) return;
           // Shell.Current.GoToAsync($"//ProjectDetail?clientId={i.Model.ClientId}&projectId={i?.Model?.Id ?? 0}");
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
 