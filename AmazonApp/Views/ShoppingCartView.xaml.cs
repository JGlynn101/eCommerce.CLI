using System.Diagnostics;
using System.Windows.Input;
using Amazon.Library.Models;
using Amazon.Library.Services;
using AmazonApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AmazonApp.Views
{
    [QueryProperty(nameof(ShopCartId), "ShopCartId")]
    public partial class ShoppingCartView : ContentPage
    {
        private int _shopCartId;
        public int ShopCartId
        {
            get => _shopCartId;
            set
            {
                _shopCartId = value;
                LoadCart(value);
            }
        }

        public ShoppingCartView()
        {
            InitializeComponent();
            BindingContext = new ShoppingCartViewModel();
        }
        private void LoadCart(int cartId)
        {
            if (cartId > 0)
            {
                BindingContext = new ShoppingCartViewModel(ShoppingCartService.Current.GetCart(cartId));
            }
            else
            {
                BindingContext = new ShoppingCartViewModel(new ShoppingCart());
            }
        }
        private async void SaveClicked(object sender, EventArgs e)
        {
            if (BindingContext is ShoppingCartViewModel viewModel)
            {
                viewModel.Add();
                await Shell.Current.GoToAsync("//ShoppingCart");
            }
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingCart");
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (BindingContext is ShoppingCartViewModel viewModel)
            {
                viewModel.RefreshItems();
            }
        }
        private async void UpdateTaxRateClicked(object sender, EventArgs e)
        {
            if (BindingContext is ShoppingCartViewModel viewModel)
            {
                await Shell.Current.GoToAsync($"//TaxView?ShopCartId={viewModel.Cart.Id}");
            }
        }

        private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {
            if (BindingContext is ShoppingCartViewModel viewModel)
            {
                viewModel.RefreshItems();
            }
        }
    }
}
