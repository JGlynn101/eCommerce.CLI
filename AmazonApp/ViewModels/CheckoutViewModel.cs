using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Amazon.Library.Models;
using Amazon.Library.Services;
using System.Linq;

namespace AmazonApp.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        private ShoppingCart _cart;
        private Customer _customer;

        public CheckoutViewModel(int shopCartId)
        {
            Cart = ShoppingCartService.Current.GetCart(shopCartId);
            Customer = new Customer(); 
            SetupCommands();
        }

        public ShoppingCart Cart
        {
            get => _cart;
            set
            {
                if (_cart != value)
                {
                    _cart = value;
                    OnPropertyChanged();
                }
            }
        }

        public Customer Customer
        {
            get => _customer;
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CompleteCheckoutCommand { get; private set; }

        private void SetupCommands()
        {
            CompleteCheckoutCommand = new Command(ExecuteCompleteCheckout);
        }

        private async void ExecuteCompleteCheckout()
        {
            if (Cart?.Contents == null || !Cart.Contents.Any())
            {
                return;
            }

            var itemsToRemove = Cart.Contents.ToList();

            foreach (var item in itemsToRemove)
            {
                var inventoryItem = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Id == item.Id);
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity -= item.Quantity;
                    if (inventoryItem.Quantity <= 0)
                    {
                        InventoryServiceProxy.Current.Items.Remove(inventoryItem);
                    }
                }
                Cart.Contents.Remove(item);
            }

            ShoppingCartService.Current.AddOrUpdateCart(Cart);

            OnPropertyChanged(nameof(Cart));
            OnPropertyChanged(nameof(Cart.Contents));
            await Shell.Current.GoToAsync("//Shop");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
