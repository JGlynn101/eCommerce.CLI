using AmazonApp.ViewModels;
namespace AmazonApp.Views
{
    [QueryProperty(nameof(ShopCartId), "ShopCartId")]
    public partial class CheckoutView : ContentPage
    {
        private int _shopCartId;

        public int ShopCartId
        {
            get => _shopCartId;
            set
            {
                _shopCartId = value;
                BindingContext = new CheckoutViewModel(ShopCartId);
            }
        }

        public CheckoutView()
        {
            InitializeComponent();
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingCart");
        }
    }
}
