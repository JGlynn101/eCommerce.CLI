using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Library.Models;
using Amazon.Library.Services;

namespace AmazonApp.ViewModels
{
    public class ShoppingCartManagementViewModel : INotifyPropertyChanged
    {
        private ShoppingCartViewModel _selectedCart;

        public ObservableCollection<ShoppingCartViewModel> Carts { get; set; }
        public ShoppingCartViewModel SelectedCart
        {
            get => _selectedCart;
            set
            {
                if (_selectedCart != value)
                {
                    _selectedCart = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Name
        {
            get
            {
                return SelectedCart?.CartName ?? string.Empty;
            }
            set
            {
                if (SelectedCart != null)
                {
                    SelectedCart.CartName = value ?? "Shopping Cart";
                }
            }
        }
        public int Id
        {
            get
            {
                return SelectedCart?.Id ?? 0;
            }
            set
            {
                if (SelectedCart != null)
                {
                    SelectedCart.Id = value;
                }
            }
        }

        public ICommand EditCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ShoppingCartManagementViewModel()
        {
            Carts = new ObservableCollection<ShoppingCartViewModel>(
                ShoppingCartService.Current.Carts.Select(s => new ShoppingCartViewModel(s)));
            SetupCommands();
        }

        private void SetupCommands()
        {
            EditCommand = new Command<ShoppingCartViewModel>(ExecuteEdit);
            DeleteCommand = new Command<ShoppingCartViewModel>(ExecuteDelete);
            AddCommand = new Command(ExecuteAdd);
        }

        private async void ExecuteEdit(ShoppingCartViewModel cartViewModel)
        {
            if (cartViewModel?.Cart == null)
            {
                return;
            }
            SelectedCart = cartViewModel;
            await Shell.Current.GoToAsync($"//Shop?ShopCartId={cartViewModel.Cart.Id}");
        }

        private async void ExecuteAdd()
        {
            var newCart = new ShoppingCart();
            var newCartViewModel = new ShoppingCartViewModel(newCart);
            Carts.Add(newCartViewModel);
            SelectedCart = newCartViewModel;
            await Shell.Current.GoToAsync("//Shop");
        }
        private void ExecuteDelete(ShoppingCartViewModel cartViewModel)
        {
            if (cartViewModel?.Cart == null)
            {
                return;
            }
            cartViewModel.ClearCart();
            ShoppingCartService.Current.RemoveCart(cartViewModel.Cart); //removes from Shoppingcart service's Current
            Carts.Remove(cartViewModel);    // removes from shoppingcartmanagementviewmodel's list of carts.
            OnPropertyChanged(nameof(Carts));
        }
        public void RefreshItems()
        {
            Carts = new ObservableCollection<ShoppingCartViewModel>(
               ShoppingCartService.Current.Carts.Select(s => new ShoppingCartViewModel(s)));
            OnPropertyChanged(nameof(Carts));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}