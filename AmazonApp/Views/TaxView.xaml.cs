using AmazonApp.ViewModels;
namespace AmazonApp.Views
{
    [QueryProperty(nameof(ShopCartId), "ShopCartId")]
    public partial class TaxView : ContentPage
    {
        private int _shopCartId;

        public int ShopCartId
        {
            get => _shopCartId;
            set
            {
                _shopCartId = value;
                BindingContext = new TaxViewModel(_shopCartId);
            }
        }

        public TaxView()
        {
            InitializeComponent();  
            BindingContext = new TaxViewModel();    
        }

        private void BackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingCart");
        }
    }
}
