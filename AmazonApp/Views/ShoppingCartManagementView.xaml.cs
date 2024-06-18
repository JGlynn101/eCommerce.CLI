using AmazonApp.ViewModels;

namespace AmazonApp.Views;

public partial class ShoppingCartManagementView : ContentPage
{
	public ShoppingCartManagementView()
	{
        InitializeComponent();
		BindingContext = new ShoppingCartManagementViewModel();
	}

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}