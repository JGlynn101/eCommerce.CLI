using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Services;
using System.Windows.Input;
using Amazon.Library.Models;

namespace AmazonApp.ViewModels
{
    public class TaxViewModel : INotifyPropertyChanged
    {
        private ShoppingCart _cart;
        private decimal _taxRate;
        private readonly int _cartId;
        
        public TaxViewModel() 
        {
            _cart = new ShoppingCart();
            _taxRate = _cart?.TaxRate ?? 0.07m;
            UpdateRateCommand = new Command(UpdateRate);
        }
        public TaxViewModel(int cartId)
        {
            _cartId = cartId;
            var cart = ShoppingCartService.Current.GetCart(cartId);
            _taxRate = cart?.TaxRate ?? 0.07m;
            UpdateRateCommand = new Command(UpdateRate);
        }

        public decimal TaxRate
        {
            get => _taxRate;
            set
            {
                if (_taxRate != value)
                {
                    _taxRate = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand UpdateRateCommand { get; }

        private void UpdateRate()
        {
            ShoppingCartService.Current.UpdateTaxRate(_cartId, _taxRate);
            Shell.Current.GoToAsync("//ShoppingCart");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
