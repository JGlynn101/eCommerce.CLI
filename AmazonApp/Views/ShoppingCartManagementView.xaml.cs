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
    private void AddCartClicked(object sender, EventArgs e)
    {
        if (BindingContext is ShoppingCartManagementViewModel viewModel)
        {
            viewModel.AddCommand.Execute(null);
        }
    }
    private async void EditCartClicked(object sender, EventArgs e)
    {
        if (BindingContext is ShoppingCartManagementViewModel viewModel && viewModel.SelectedCart != null)
        {
            var selectedCartId = viewModel.SelectedCart.Cart.Id;
            await Shell.Current.GoToAsync($"//Shop?ShopCartId={selectedCartId}");
        }
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (BindingContext is ShoppingCartManagementViewModel viewModel)
        {
            viewModel.RefreshItems();
        }
    }
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
}