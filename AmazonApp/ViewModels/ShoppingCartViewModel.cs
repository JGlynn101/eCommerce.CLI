using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Library.Models;
using System.Windows.Input;
using Amazon.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace AmazonApp.ViewModels
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        private ShoppingCart _cart;
        private ItemViewModel _selectedItem;
        private ItemViewModel _selectedInventoryItem;
        private int _selectedQuantity;
        private int _selectedQuantitytoDelete;

        private decimal _taxRate;
        public ShoppingCart Cart
        {
            get => _cart;
            set
            {
                if (_cart != value)
                {
                    _cart = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(CartName));
                }
            }
        }
        public string CartName
        {
            get => _cart?.Name ?? string.Empty;
            set
            {
                if (_cart != null && _cart.Name != value)
                {
                    _cart.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id
        {
            get => _cart?.Id ?? 0;
            set
            {
                if (_cart != null && _cart.Id != value)
                {
                    _cart.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal TaxRate
        {
            get => _cart.TaxRate;
        }

        public ObservableCollection<ItemViewModel> Items
        {
            get => new ObservableCollection<ItemViewModel>(Cart?.Contents?.Select(i => new ItemViewModel(i)) ?? new ObservableCollection<ItemViewModel>());
        }
        public List<ItemViewModel> InventoryItems
        {
            get
            {
                return InventoryServiceProxy.Current.Items.Where(i => i != null).Select(i => new ItemViewModel(i)).ToList() ?? new List<ItemViewModel>();
            }

        }
        public decimal total
        {
            get
            {
                decimal subtotal = 0;

                if (Cart?.Contents != null)
                {
                    foreach (var item in Cart.Contents)
                    {
                        decimal discount = Math.Clamp(item.Discount, 0.00m, 1.00m); // Limits discount to this range

                        decimal discountedPrice = item.Price * (1 - discount);

                        if (item.BOGO)
                        {
                            // For BOGO, every 2 items count as 1
                            int chargeableQuantity = item.Quantity / 2 + item.Quantity % 2;
                            subtotal += chargeableQuantity * discountedPrice;
                        }
                        else
                        {
                            subtotal += item.Quantity * discountedPrice;
                        }
                    }
                }

                decimal tax = subtotal * TaxRate;
                return subtotal + tax;
            }
        }

        public ItemViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }
        public ItemViewModel SelectedInventoryItem
        {
            get => _selectedInventoryItem;
            set
            {
                if (_selectedInventoryItem != value)
                {
                    _selectedInventoryItem = value;
                    OnPropertyChanged();
                }
            }
        }
        public int SelectedQuantity //to add to Shopping Cart
        {
            get => _selectedQuantity;
            set
            {
                if (_selectedQuantity != value)
                {
                    _selectedQuantity = value;
                    OnPropertyChanged();
                }
            }
        }
        public int SelectedQuantitytoDelete //from shopping cart
        {
            get => _selectedQuantitytoDelete;
            set
            {
                if (_selectedQuantitytoDelete != value)
                {
                    _selectedQuantitytoDelete = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand PurchaseCommand { get; private set; }
        public ICommand CheckoutCommand { get; private set; }

        public ShoppingCartViewModel(int cartId)
        {
            Cart = ShoppingCartService.Current.Carts.FirstOrDefault(c => c.Id == cartId) ?? new ShoppingCart();
            SetupCommands();
        }
        public ShoppingCartViewModel()
        {
            Cart = new ShoppingCart();
            SetupCommands();
        }
        public ShoppingCartViewModel(ShoppingCart cart)
        {
            Cart = cart ?? new ShoppingCart();
            SetupCommands();
        }
        private void SetupCommands()
        {
            AddCommand = new Command(ExecuteAdd);
            DeleteCommand = new Command<ItemViewModel>(ExecuteDelete);
            RemoveCommand = new Command<ItemViewModel>(ExecuteRemove);
            EditCommand = new Command(ExecuteEdit);
            CheckoutCommand = new Command(ExecuteCheckout);
            PurchaseCommand = new Command(ExecutePurchase);
        }
        private async void ExecutePurchase()
        {
            if (Cart != null)
            {
                await Shell.Current.GoToAsync($"//Inventory?ShopCartId={Cart.Id}");
            }
        }

        private void ExecuteAdd()
        {
            if (SelectedInventoryItem != null && SelectedQuantity > 0)
            {
                ShoppingCartService.Current.AddToCart(Cart.Id, new Item
                {
                    Id = SelectedInventoryItem.Model.Id,
                    Name = SelectedInventoryItem.Model.Name,
                    Description = SelectedInventoryItem.Model.Description,
                    Price = SelectedInventoryItem.Model.Price,
                    Quantity = SelectedQuantity, Discount = SelectedInventoryItem.Model.Discount,
                    BOGO = SelectedInventoryItem.Model.BOGO
                });
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(total));
            }
        }
        private void ExecuteRemove(ItemViewModel item)
        {
            if (item != null && SelectedQuantitytoDelete > 0)
            {
                var cartItem = Cart.Contents.FirstOrDefault(i => i.Id == item.Model.Id);
                var invItem = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Name == item.Model.Name);
                if (cartItem != null && invItem != null)
                {
                    int quantityToRemove = Math.Min(cartItem.Quantity, SelectedQuantitytoDelete);
                    invItem.Quantity += quantityToRemove;
                    cartItem.Quantity -= quantityToRemove;

                    if (cartItem.Quantity <= 0)
                    {
                        Cart.Contents.Remove(cartItem);
                    }

                    OnPropertyChanged(nameof(Items));
                    OnPropertyChanged(nameof(total));
                }
            }
        }
        private async void ExecuteCheckout()
        {
            await Shell.Current.GoToAsync($"//CheckoutView?ShopCartId={Cart.Id}");
        }
        private void ExecuteDelete(ItemViewModel item)
        {
            if (item != null)
            {
                var cartItem = Cart.Contents.FirstOrDefault(i => i.Id == item.Model.Id);
                var invItem = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Name == item.Model.Name);
                if (cartItem != null && invItem != null)
                {
                    invItem.Quantity += cartItem.Quantity;
                    Cart.Contents.Remove(cartItem);
                }
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(total));
            }
        }
        private void ExecuteEdit()
        {
            ShoppingCartService.Current.AddOrUpdateCart(Cart);
            Shell.Current.GoToAsync("//ShoppingCart");
        }
        public void Add()
        {
            ShoppingCartService.Current.AddOrUpdateCart(Cart);
        }
        public void ClearCart()
        {
            foreach (var item in Cart.Contents.ToList())
            {
                ExecuteDelete(new ItemViewModel(item));
            }
        }

        public void RefreshItems()
        {
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(total));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
