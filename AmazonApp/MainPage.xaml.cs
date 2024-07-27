using AmazonApp.ViewModels;
using Microsoft.Maui.Controls;

namespace AmazonApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void CustomerClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingCart");
        }

        private void EmployeeClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        } 


    }

}
